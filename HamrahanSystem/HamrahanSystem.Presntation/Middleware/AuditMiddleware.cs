using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.UseCaseInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;

namespace HamrahanSystem.Presntation.Middleware
{
	public sealed class AuditMiddleware
	{
		private const int MaxBodyLength = 4096;

		private static readonly HashSet<string> SensitiveKeys = new(StringComparer.OrdinalIgnoreCase)
		{
			"password",
			"passwordhash",
			"passwordsalt",
			"token",
			"accesstoken",
			"refreshtoken",
			"secret",
			"apikey"
		};

		private static readonly HashSet<string> StaticExtensions = new(StringComparer.OrdinalIgnoreCase)
		{
			".css",
			".js",
			".map",
			".png",
			".jpg",
			".jpeg",
			".gif",
			".svg",
			".ico",
			".woff",
			".woff2",
			".ttf",
			".eot",
			".pdf"
		};

		private readonly RequestDelegate _next;
		public AuditMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(
			HttpContext context,
			IUserActivityLogService logService,
			IAuditContext auditContext,
			ICurrentUserService currentUserService)
		{
			if (ShouldSkip(context))
			{
				await _next(context);
				return;
			}

			var correlationId = Guid.NewGuid();
			auditContext.CorrelationId = correlationId;

			var stopwatch = Stopwatch.StartNew();
			var requestBody = await ReadRequestBodyAsync(context.Request);

			try
			{
				await _next(context);
			}
			finally
			{
				stopwatch.Stop();
				await SaveLogAsync(context, correlationId, requestBody, (int)stopwatch.ElapsedMilliseconds, logService, currentUserService);
			}
		}

		private static async Task SaveLogAsync(
			HttpContext context,
			Guid correlationId,
			string? requestBody,
			int durationMs,
			IUserActivityLogService logService,
			ICurrentUserService currentUserService)
		{
			var routeData = context.GetRouteData();
			var controller = routeData?.Values["controller"]?.ToString();
			var action = routeData?.Values["action"]?.ToString();

			var actionType = GetActionType(controller, action, context.Request.Method);
			var formName = controller != null && action != null ? $"{controller}/{action}" : context.Request.Path.Value;

			var log = new UserActivityLogDto
			{
				CorrelationId = correlationId,
				UserId = currentUserService.UserId,
				UserName = currentUserService.UserName,
				ActionType = actionType,
				Controller = controller,
				Action = action,
				FormName = formName,
				HttpMethod = context.Request.Method,
				Path = context.Request.Path.Value,
				QueryString = context.Request.QueryString.HasValue ? context.Request.QueryString.Value : null,
				RequestBody = requestBody,
				StatusCode = context.Response?.StatusCode,
				DurationMs = durationMs,
				IpAddress = BuildIpAddress(context),
				UserAgent = context.Request.Headers["User-Agent"].ToString(),
				CreatedAt = DateTime.Now
			};

			await logService.Add(log);
		}

		private static string GetActionType(string? controller, string? action, string method)
		{
			if (string.Equals(controller, "Account", StringComparison.OrdinalIgnoreCase))
			{
				if (string.Equals(action, "LogIn", StringComparison.OrdinalIgnoreCase) && HttpMethods.IsPost(method))
				{
					return "Login";
				}
				if (string.Equals(action, "Logout", StringComparison.OrdinalIgnoreCase))
				{
					return "Logout";
				}
			}

			return "Request";
		}

		private static bool ShouldSkip(HttpContext context)
		{
			var path = context.Request.Path.Value ?? string.Empty;
			if (path.StartsWith("/assets", StringComparison.OrdinalIgnoreCase) ||
				path.StartsWith("/lib", StringComparison.OrdinalIgnoreCase) ||
				path.StartsWith("/favicon", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}

			var extension = Path.GetExtension(path);
			return !string.IsNullOrWhiteSpace(extension) && StaticExtensions.Contains(extension);
		}

		private static async Task<string?> ReadRequestBodyAsync(HttpRequest request)
		{
			if (HttpMethods.IsGet(request.Method) || HttpMethods.IsHead(request.Method))
			{
				return null;
			}

			if (request.ContentLength.HasValue && request.ContentLength.Value == 0)
			{
				return null;
			}

			if (request.ContentType == null)
			{
				return null;
			}

			if (request.ContentType.StartsWith("multipart/", StringComparison.OrdinalIgnoreCase))
			{
				return "[multipart]";
			}

			if (request.HasFormContentType)
			{
				request.EnableBuffering();
				var form = await request.ReadFormAsync();
				request.Body.Position = 0;

				var pairs = form.Select(item =>
				{
					var value = string.Join(",", item.Value.ToArray());
					return $"{item.Key}={MaskIfSensitive(item.Key, value)}";
				});

				return string.Join("&", pairs);
			}

			request.EnableBuffering();
			using var reader = new StreamReader(
				request.Body,
				Encoding.UTF8,
				detectEncodingFromByteOrderMarks: false,
				bufferSize: 1024,
				leaveOpen: true);

			var bufferLength = (int)Math.Min(request.ContentLength ?? MaxBodyLength, MaxBodyLength);
			var buffer = new char[bufferLength];
			var read = await reader.ReadBlockAsync(buffer, 0, buffer.Length);
			request.Body.Position = 0;

			if (read <= 0)
			{
				return null;
			}

			var body = new string(buffer, 0, read);
			if (IsJson(request.ContentType))
			{
				return MaskJson(body);
			}

			return MaskKeyValue(body);
		}

		private static string? BuildIpAddress(HttpContext context)
		{
			var candidates = new List<string>();
			AddHeaderCandidates(context, "X-Forwarded-For", candidates);
			AddHeaderCandidates(context, "X-Real-IP", candidates);
			AddHeaderCandidates(context, "CF-Connecting-IP", candidates);
			AddHeaderCandidates(context, "X-Client-IP", candidates);

			var remoteIp = context.Connection.RemoteIpAddress?.ToString();
			if (!string.IsNullOrWhiteSpace(remoteIp))
			{
				candidates.Add(remoteIp);
			}

			string? publicIp = null;
			string? privateIp = null;

			foreach (var candidate in candidates)
			{
				var ipv4 = NormalizeToIpv4(candidate);
				if (ipv4 == null)
				{
					continue;
				}

				if (IsPrivateIpv4(ipv4))
				{
					privateIp ??= ipv4;
				}
				else
				{
					publicIp ??= ipv4;
				}
			}

			if (string.IsNullOrWhiteSpace(publicIp) && string.IsNullOrWhiteSpace(privateIp))
			{
				return null;
			}

			if (!string.IsNullOrWhiteSpace(publicIp) && string.Equals(publicIp, privateIp, StringComparison.OrdinalIgnoreCase))
			{
				privateIp = null;
			}

			var parts = new List<string>();
			if (!string.IsNullOrWhiteSpace(publicIp))
			{
				parts.Add($"public={publicIp}");
			}
			if (!string.IsNullOrWhiteSpace(privateIp))
			{
				parts.Add($"private={privateIp}");
			}

			return string.Join(";", parts);
		}

		private static void AddHeaderCandidates(HttpContext context, string headerName, List<string> candidates)
		{
			if (!context.Request.Headers.TryGetValue(headerName, out var values))
			{
				return;
			}

			foreach (var value in values)
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					continue;
				}

				var parts = value.Split(',', StringSplitOptions.RemoveEmptyEntries);
				foreach (var part in parts)
				{
					var trimmed = part.Trim();
					if (!string.IsNullOrWhiteSpace(trimmed))
					{
						candidates.Add(trimmed);
					}
				}
			}
		}

		private static string? NormalizeToIpv4(string? value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return null;
			}

			var trimmed = value.Trim();
			if (trimmed.StartsWith("[", StringComparison.Ordinal) && trimmed.Contains("]"))
			{
				var endIndex = trimmed.IndexOf(']');
				if (endIndex > 1)
				{
					trimmed = trimmed.Substring(1, endIndex - 1);
				}
			}

			if (trimmed.Contains('.') && trimmed.Count(c => c == ':') == 1)
			{
				trimmed = trimmed.Split(':')[0];
			}

			if (!IPAddress.TryParse(trimmed, out var ip))
			{
				return null;
			}

			if (ip.AddressFamily == AddressFamily.InterNetwork)
			{
				return ip.ToString();
			}

			if (ip.AddressFamily == AddressFamily.InterNetworkV6)
			{
				if (IPAddress.IPv6Loopback.Equals(ip))
				{
					return "127.0.0.1";
				}

				if (ip.IsIPv4MappedToIPv6)
				{
					return ip.MapToIPv4().ToString();
				}
			}

			return null;
		}

		private static bool IsPrivateIpv4(string ipv4)
		{
			if (!IPAddress.TryParse(ipv4, out var ip))
			{
				return false;
			}

			var bytes = ip.GetAddressBytes();
			if (bytes.Length != 4)
			{
				return false;
			}

			if (bytes[0] == 10)
			{
				return true;
			}

			if (bytes[0] == 172 && bytes[1] >= 16 && bytes[1] <= 31)
			{
				return true;
			}

			if (bytes[0] == 192 && bytes[1] == 168)
			{
				return true;
			}

			if (bytes[0] == 127)
			{
				return true;
			}

			if (bytes[0] == 169 && bytes[1] == 254)
			{
				return true;
			}

			return false;
		}

		private static bool IsJson(string contentType)
		{
			return contentType.Contains("application/json", StringComparison.OrdinalIgnoreCase);
		}

		private static string MaskJson(string body)
		{
			try
			{
				var node = JsonNode.Parse(body);
				MaskJsonNode(node);
				return node?.ToJsonString(new JsonSerializerOptions { WriteIndented = false }) ?? body;
			}
			catch
			{
				return MaskKeyValue(body);
			}
		}

		private static void MaskJsonNode(JsonNode? node)
		{
			if (node is JsonObject obj)
			{
				foreach (var property in obj.ToList())
				{
					var key = property.Key;
					if (SensitiveKeys.Contains(key))
					{
						obj[key] = "***";
						continue;
					}

					MaskJsonNode(property.Value);
				}
			}
			else if (node is JsonArray arr)
			{
				foreach (var item in arr)
				{
					MaskJsonNode(item);
				}
			}
		}

		private static string MaskKeyValue(string body)
		{
			if (string.IsNullOrWhiteSpace(body))
			{
				return body;
			}

			try
			{
				var normalized = body.StartsWith("?", StringComparison.Ordinal) ? body : $"?{body}";
				var query = QueryHelpers.ParseQuery(normalized);
				var pairs = query.Select(kvp =>
				{
					var value = string.Join(",", kvp.Value.ToArray());
					return $"{kvp.Key}={MaskIfSensitive(kvp.Key, value)}";
				});
				return string.Join("&", pairs);
			}
			catch
			{
				return body;
			}
		}

		private static string MaskIfSensitive(string key, string value)
		{
			return SensitiveKeys.Contains(key) ? "***" : value;
		}
	}
}
