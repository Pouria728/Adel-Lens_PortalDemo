using HamrahanSystem.Application.UseCaseInterface;
using Microsoft.AspNetCore.Http;

namespace HamrahanSystem.Presntation.Services
{
	public sealed class CurrentUserService : ICurrentUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CurrentUserService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public int? UserId
		{
			get
			{
				var context = _httpContextAccessor.HttpContext;
				if (context == null)
				{
					return null;
				}

				var sessionUserId = context.Session.Get<int>("UserId");
				if (sessionUserId != 0)
				{
					return sessionUserId;
				}

				var claimValue = context.User?.FindFirst("UserId")?.Value;
				if (int.TryParse(claimValue, out var userId))
				{
					return userId;
				}

				return null;
			}
		}

		public string? UserName
		{
			get
			{
				var context = _httpContextAccessor.HttpContext;
				if (context == null)
				{
					return null;
				}

				var sessionName = context.Session.Get<string>("UserName");
				if (!string.IsNullOrWhiteSpace(sessionName))
				{
					return sessionName;
				}

				return context.User?.FindFirst("UserName")?.Value ?? context.User?.Identity?.Name;
			}
		}

		public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
	}
}
