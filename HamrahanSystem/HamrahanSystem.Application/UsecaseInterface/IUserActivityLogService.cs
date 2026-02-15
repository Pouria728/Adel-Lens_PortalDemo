using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
	public partial interface IUserActivityLogService
	{
		Task Add(UserActivityLogDto dto);
		Task AddDetails(List<UserActivityLogDetailDto> items);
		Task<(List<UserActivityLogDto>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx, DateTime? fromDate, DateTime? toDate, string? userName);
		Task<UserActivityLogDto> GetByCorrelationId(Guid correlationId);
		Task<List<UserActivityLogDetailDto>> GetDetails(Guid correlationId);
	}
}
