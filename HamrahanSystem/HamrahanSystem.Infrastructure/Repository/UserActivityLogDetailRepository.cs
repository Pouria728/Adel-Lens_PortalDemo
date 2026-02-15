using HamrahanSystem.Domain.Entity;
using HamrahanSystem.Domain.Repository;

namespace HamrahanSystem.Infrastructure.Repository
{
	public partial class UserActivityLogDetailRepository : EntityFrameworkRepository<UserActivityLogDetail>, IUserActivityLogDetailRepository
	{
		public UserActivityLogDetailRepository(AdelModel context)
			: base(context)
		{
		}

		public Task AddRange(List<UserActivityLogDetail> items)
		{
			objectSet.AddRange(items);
			Save();
			return Task.CompletedTask;
		}

		public Task<List<UserActivityLogDetail>> GetByCorrelationId(Guid correlationId)
		{
			return Task.FromResult(objectSet.Where(x => x.CorrelationId == correlationId).ToList());
		}

		public new AdelModel Context
		{
			get { return (AdelModel)base.Context; }
		}
	}
}
