using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
	public partial interface IUserActivityLogRepository : IRepository<UserActivityLog>
	{
		Task Add(UserActivityLog log);
		Task<(List<UserActivityLog>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx, DateTime? fromDate, DateTime? toDate, string? userName);
		Task<UserActivityLog> GetById(long id);
		Task<UserActivityLog> GetByCorrelationId(Guid correlationId);
	}
}
