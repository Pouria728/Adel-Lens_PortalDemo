using HamrahanSystem.Domain.Entity;
using HamrahanSystem.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace HamrahanSystem.Infrastructure.Repository
{
	public partial class UserActivityLogRepository : EntityFrameworkRepository<UserActivityLog>, IUserActivityLogRepository
	{
		public UserActivityLogRepository(AdelModel context)
			: base(context)
		{
		}

		public Task Add(UserActivityLog log)
		{
			objectSet.Add(log);
			Save();
			return Task.CompletedTask;
		}

		public Task<(List<UserActivityLog>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx, DateTime? fromDate, DateTime? toDate, string? userName)
		{
			var item = objectSet.AsQueryable();

			if (fromDate.HasValue)
			{
				item = item.Where(x => x.CreatedAt >= fromDate.Value);
			}
			if (toDate.HasValue)
			{
				item = item.Where(x => x.CreatedAt <= toDate.Value);
			}
			if (!string.IsNullOrWhiteSpace(userName))
			{
				item = item.Where(x => x.UserName != null && x.UserName.Contains(userName));
			}

			var countList = item.Count();

			if (!string.IsNullOrEmpty(sidx))
			{
				var colsort = (new UserActivityLog()).GetType().GetMembers()
					.SingleOrDefault(y => y.Name.Equals(sidx, StringComparison.OrdinalIgnoreCase))?.Name;
				if (colsort != null)
				{
					item = sort == "asc"
						? item.OrderBy(x => EF.Property<object>(x, colsort))
						: item.OrderByDescending(x => EF.Property<object>(x, colsort));
				}
			}
			else
			{
				item = item.OrderByDescending(x => x.CreatedAt);
			}

			if (maxResult.HasValue && maxResult > 0)
			{
				item = item.Take(maxResult.Value);
			}
			if (page.HasValue && rowInPage.HasValue && countList > 0)
			{
				int totalPages = (int)Math.Ceiling((float)countList / (float)rowInPage);
				page = page.Value > totalPages ? totalPages : page.Value;
				int countIndex = ((page.Value - 1) * rowInPage.Value) + rowInPage.Value > countList
					? countList - ((page.Value - 1) * rowInPage.Value)
					: rowInPage.Value;
				item = item.Skip((page.Value - 1) * rowInPage.Value).Take(countIndex);
			}

			return Task.FromResult((item.ToList(), countList));
		}

		public Task<UserActivityLog> GetById(long id)
		{
			return Task.FromResult(objectSet.SingleOrDefault(x => x.ActivityLogId == id));
		}

		public Task<UserActivityLog> GetByCorrelationId(Guid correlationId)
		{
			return Task.FromResult(objectSet.Include(x => x.Details).SingleOrDefault(x => x.CorrelationId == correlationId));
		}

		public new AdelModel Context
		{
			get { return (AdelModel)base.Context; }
		}
	}
}
