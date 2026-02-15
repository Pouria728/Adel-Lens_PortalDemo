
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class BaseRequestFieldRepository : EntityFrameworkRepository<BaseRequestField>, IBaseRequestFieldRepository
    {
        public BaseRequestFieldRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<BaseRequestField> GetAll()
        {
            return objectSet.ToList();
        }
		public virtual Task<(List<BaseRequestField>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Count();
			var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new BaseRequestField()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
                if (colsort != null)
                    item = sort == "asc" ? item.OrderBy(x => EF.Property<object>(x, colsort)) : item.OrderByDescending(x => EF.Property<object>(x, colsort));

            }
            if (maxResult.HasValue && maxResult > 0)
			{
				item = item.Take(maxResult.Value);
			}
			if (page.HasValue && rowInPage.HasValue && countList > 0)
			{
				int totalPages = (int)Math.Ceiling((float)countList / (float)rowInPage);
				page = page.Value > totalPages ? totalPages : page.Value;
				int CountIndexs = ((page.Value - 1) * rowInPage.Value) + rowInPage.Value > countList ? countList - ((page.Value - 1) * rowInPage.Value) : rowInPage.Value;
				item = item.Skip((page.Value - 1) * rowInPage.Value).Take(CountIndexs);
			}
  
            return Task.FromResult((item.ToList(), countList));

		}

		public virtual BaseRequestField GetByKey(int _BaseRequestFieldedId)
        {
            return objectSet.Include(x=>x.BaseRequeste).Include(x=>x.BaseRequestFielde_BaseRequestParentFieldId).SingleOrDefault(e => e.BaseRequestFieldId == _BaseRequestFieldedId);
        }
		public Task Update(BaseRequestField BaseRequestFielded)
		{
			var item = objectSet.Single(x => x.BaseRequestFieldId == BaseRequestFielded.BaseRequestFieldId);
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.BaseRequestFieldId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<BaseRequestField> BaseRequestFieldeds)
		{
			objectSet.RemoveRange(BaseRequestFieldeds);
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<BaseRequestField> BaseRequestFieldeds)
		{
			throw new NotImplementedException();
		}

		public Task Add(BaseRequestField BaseRequestFielded)
		{

			objectSet.Add(BaseRequestFielded);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<BaseRequestField> BaseRequestFieldeds)
		{
			objectSet.AddRange(BaseRequestFieldeds);
			Save();
			return Task.CompletedTask;
		}

		public new AdelModel Context 
        {
            get
            {
                return (AdelModel)base.Context;
            }
        }
    }
}
