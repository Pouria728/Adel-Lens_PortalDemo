using System;

namespace HamrahanSystem.Application.UseCaseInterface
{
	public interface ICurrentUserService
	{
		int? UserId { get; }
		string? UserName { get; }
		bool IsAuthenticated { get; }
	}
}
