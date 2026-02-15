using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.UseCaseInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Text;

namespace HamrahanSystem.Presntation.Controllers
{
	[Authorize]
	public class UserActivityLogsController(IUserActivityLogService logService, IUserService userService) : Controller
	{
		private static readonly Dictionary<string, string> ControllerTitles = new(StringComparer.OrdinalIgnoreCase)
		{
			["UserActivityLogs"] = "گزارش فعالیت کاربران",
			["Users"] = "کاربران",
			["Roles"] = "نقش‌ها",
			["Orders"] = "سفارشات",
			["Home"] = "داشبورد",
			["Account"] = "حساب کاربری"
		};

		private static readonly Dictionary<string, string> ActionTitles = new(StringComparer.OrdinalIgnoreCase)
		{
			["Index"] = "لیست",
			["Create"] = "ایجاد",
			["Edit"] = "ویرایش",
			["Delete"] = "حذف",
			["Details"] = "جزئیات",
			["LogIn"] = "ورود",
			["Login"] = "ورود",
			["Logout"] = "خروج",
			["OrderStock"] = "سفارش انبار",
			["OrderStockGranty"] = "سفارش انبار (گارانتی)",
			["ListOrders"] = "لیست سفارشات",
			["ListReqOrders"] = "لیست سفارشات سفارشی",
			["OrderSpecial"] = "سفارش ویژه"
		};

		[Authorize(Roles = "admin,Audit_Index")]
		public IActionResult Index()
		{
			return View();
		}

		[Authorize(Roles = "admin,Audit_Index")]
		[HttpPost]
		public async Task<JsonResult> Detail()
		{
			var itemGrid = await TryReadGridDtoAsync();
			if (itemGrid == null)
			{
				return Json(new { total = 0, page = 1, records = 0, rows = Array.Empty<object>() });
			}

			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;
			int totalRecords = 0;

			var item = await logService.GetAll(null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx, null, null, null);
			totalRecords = item.Item2;
			if (totalRecords <= 0)
			{
				return Json(new { total = 0, page = itemGrid.Page, records = 0, rows = Array.Empty<object>() });
			}

			var userNameLookup = await BuildUserNameLookupAsync(item.Item1);
			int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
			var jsonData = new
			{
				total = totalPages,
				page = itemGrid.Page,
				records = totalRecords,
				rows = from p in item.Item1
					   let ipPair = ParseIpAddress(p.IpAddress)
					   select new
					   {
						   id = p.ActivityLogId,
						   createdDate = ToPersianDate(p.CreatedAt),
						   createdTime = ToTime(p.CreatedAt),
						   userName = ResolveUserName(p, userNameLookup),
						   actionType = MapActionType(p.ActionType),
						   formName = MapFormName(p),
						   httpMethod = MapHttpMethod(p.HttpMethod),
						   statusText = MapStatusText(p.StatusCode),
						   durationMs = p.DurationMs,
						   ipPublic = ipPair.PublicIp ?? "-",
						   ipPrivate = ipPair.PrivateIp ?? "-",
						   correlationId = p.CorrelationId
					   }
			};

			return Json(jsonData);
		}

		private static string MapActionType(string? actionType)
		{
			return actionType switch
			{
				"Login" => "ورود",
				"Logout" => "خروج",
				"Request" => "درخواست",
				_ => actionType ?? "-"
			};
		}

		private static string MapStatusText(int? statusCode)
		{
			if (!statusCode.HasValue)
			{
				return "نامشخص";
			}

			return statusCode.Value switch
			{
				200 => "موفق",
				201 => "ایجاد شد",
				204 => "بدون محتوا",
				301 => "انتقال دائم",
				302 => "انتقال موقت",
				400 => "درخواست نامعتبر",
				401 => "عدم احراز هویت",
				403 => "عدم دسترسی",
				404 => "یافت نشد",
				409 => "تعارض",
				500 => "خطای سرور",
				502 => "خطای درگاه",
				503 => "سرویس در دسترس نیست",
				_ => statusCode.Value switch
				{
					>= 200 and <= 299 => "موفق",
					>= 300 and <= 399 => "ریدایرکت",
					>= 400 and <= 499 => "خطای درخواست",
					>= 500 and <= 599 => "خطای سرور",
					_ => statusCode.Value.ToString()
				}
			};
		}

		private static string ToPersianDate(DateTime value)
		{
			var calendar = new PersianCalendar();
			var year = calendar.GetYear(value);
			var month = calendar.GetMonth(value);
			var day = calendar.GetDayOfMonth(value);
			return $"{year:0000}/{month:00}/{day:00}";
		}

		private static string ToTime(DateTime value)
		{
			return value.ToString("HH:mm:ss");
		}

		private static string ToPersianDateTime(DateTime value)
		{
			return $"{ToPersianDate(value)} {ToTime(value)}";
		}

		private static (string? PublicIp, string? PrivateIp) ParseIpAddress(string? value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return (null, null);
			}

			string? publicIp = null;
			string? privateIp = null;

			var segments = value.Split(new[] { ';', '|', ',' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var segment in segments)
			{
				var part = segment.Trim();
				if (part.StartsWith("public=", StringComparison.OrdinalIgnoreCase))
				{
					publicIp = part.Substring("public=".Length).Trim();
					continue;
				}
				if (part.StartsWith("private=", StringComparison.OrdinalIgnoreCase))
				{
					privateIp = part.Substring("private=".Length).Trim();
					continue;
				}

				var ipv4 = NormalizeToIpv4(part);
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

			if (!string.IsNullOrWhiteSpace(publicIp) && string.Equals(publicIp, privateIp, StringComparison.OrdinalIgnoreCase))
			{
				privateIp = null;
			}

			return (publicIp, privateIp);
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

			if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
			{
				return ip.ToString();
			}

			if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
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

		private static string MapHttpMethod(string? method)
		{
			if (string.IsNullOrWhiteSpace(method))
			{
				return "-";
			}

			return method.ToUpperInvariant() switch
			{
				"GET" => "دریافت",
				"POST" => "ارسال",
				"PUT" => "به‌روزرسانی",
				"DELETE" => "حذف",
				"PATCH" => "ویرایش جزئی",
				"OPTIONS" => "گزینه‌ها",
				"HEAD" => "سرصفحه",
				"TRACE" => "ردیابی",
				_ => method
			};
		}

		private static string ResolveUserName(UserActivityLogDto log, IReadOnlyDictionary<int, string> userNameLookup)
		{
			if (log.UserId.HasValue && userNameLookup.TryGetValue(log.UserId.Value, out var userName))
			{
				if (!string.IsNullOrWhiteSpace(userName))
				{
					return userName.Trim();
				}
			}

			return string.IsNullOrWhiteSpace(log.UserName) ? "-" : log.UserName.Trim();
		}

		private async Task<Dictionary<int, string>> BuildUserNameLookupAsync(IEnumerable<UserActivityLogDto> logs)
		{
			var lookup = new Dictionary<int, string>();
			var userIds = logs.Select(x => x.UserId)
				.Where(x => x.HasValue)
				.Select(x => x.Value)
				.Distinct()
				.ToList();

			foreach (var userId in userIds)
			{
				var user = await userService.GetById(userId);
				if (user != null && !string.IsNullOrWhiteSpace(user.Username))
				{
					lookup[userId] = user.Username;
				}
			}

			return lookup;
		}

		private async Task<GridDto?> TryReadGridDtoAsync()
		{
			if (Request.HasFormContentType && Request.Form.Count > 0)
			{
				var formPayload = Request.Form.First().Key;
				if (!string.IsNullOrWhiteSpace(formPayload))
				{
					return JsonConvert.DeserializeObject<GridDto>(formPayload);
				}
			}

			if (Request.ContentLength.GetValueOrDefault() == 0)
			{
				return null;
			}

			Request.EnableBuffering();
			Request.Body.Position = 0;
			using var reader = new StreamReader(Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
			var body = await reader.ReadToEndAsync();
			Request.Body.Position = 0;

			if (string.IsNullOrWhiteSpace(body))
			{
				return null;
			}

			return JsonConvert.DeserializeObject<GridDto>(body);
		}

		[Authorize(Roles = "admin,Audit_Index")]
		public IActionResult Details(string id)
		{
			if (!Guid.TryParse(id, out var correlationId))
			{
				return BadRequest();
			}

			var item = logService.GetByCorrelationId(correlationId).Result;
			if (item == null)
			{
				return NotFound();
			}

			ViewBag.FormTitle = MapFormName(item);
			ViewBag.HttpMethodTitle = MapHttpMethod(item.HttpMethod);
			ViewBag.PersianCreatedAt = ToPersianDateTime(item.CreatedAt);
			var ipPair = ParseIpAddress(item.IpAddress);
			ViewBag.PublicIp = ipPair.PublicIp ?? "-";
			ViewBag.PrivateIp = ipPair.PrivateIp ?? "-";
			return View(item);
		}

		private static string MapFormName(UserActivityLogDto log)
		{
			var controller = log.Controller;
			var action = log.Action;
			var parsedFromFormName = false;
			if (string.IsNullOrWhiteSpace(controller) || string.IsNullOrWhiteSpace(action))
			{
				(controller, action) = ParseFormName(log.FormName);
				parsedFromFormName = true;
			}

			if (string.IsNullOrWhiteSpace(controller))
			{
				return string.IsNullOrWhiteSpace(log.FormName) ? "-" : log.FormName;
			}

			var hasControllerMapping = ControllerTitles.ContainsKey(controller);
			var hasActionMapping = !string.IsNullOrWhiteSpace(action) && ActionTitles.ContainsKey(action);
			if (parsedFromFormName && !hasControllerMapping && !hasActionMapping)
			{
				return string.IsNullOrWhiteSpace(log.FormName) ? "-" : log.FormName;
			}

			var controllerTitle = MapControllerTitle(controller);
			var actionTitle = MapActionTitle(action);
			if (string.IsNullOrWhiteSpace(actionTitle))
			{
				return controllerTitle;
			}

			return $"{controllerTitle} / {actionTitle}";
		}

		private static (string? Controller, string? Action) ParseFormName(string? formName)
		{
			if (string.IsNullOrWhiteSpace(formName))
			{
				return (null, null);
			}

			var parts = formName.Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length >= 2)
			{
				return (parts[0], parts[1]);
			}

			return (parts.Length == 1 ? parts[0] : null, null);
		}

		private static string MapControllerTitle(string controller)
		{
			return ControllerTitles.TryGetValue(controller, out var title) ? title : controller;
		}

		private static string? MapActionTitle(string? action)
		{
			if (string.IsNullOrWhiteSpace(action))
			{
				return null;
			}

			return ActionTitles.TryGetValue(action, out var title) ? title : action;
		}
	}
}
