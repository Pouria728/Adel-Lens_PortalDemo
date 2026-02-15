using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
	public partial interface IUserActivityLogDetailRepository : IRepository<UserActivityLogDetail>
	{
		Task AddRange(List<UserActivityLogDetail> items);
		Task<List<UserActivityLogDetail>> GetByCorrelationId(Guid correlationId);
	}
}
