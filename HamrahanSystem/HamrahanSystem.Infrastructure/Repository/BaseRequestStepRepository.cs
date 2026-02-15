
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class BaseRequestStepRepository : EntityFrameworkRepository<BaseRequestStep>, IBaseRequestStepRepository
    {
        public BaseRequestStepRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<BaseRequestStep> GetAll()
        {
            return objectSet.ToList();
        }
		public virtual Task<(List<BaseRequestStep>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Count();
			var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new BaseRequestStep()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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

		public virtual BaseRequestStep GetByKey(int _BaseRequestStepId)
        {
            return objectSet.Include(x=>x.BaseRequeste).SingleOrDefault(e => e.BaseRequestStepId == _BaseRequestStepId);
        }
		public Task Update(BaseRequestStep BaseRequestStep)
		{
			var item = objectSet.Single(x => x.BaseRequestStepId == BaseRequestStep.BaseRequestStepId);
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.BaseRequestStepId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<BaseRequestStep> BaseRequestSteps)
		{
			objectSet.RemoveRange(BaseRequestSteps);
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<BaseRequestStep> BaseRequestSteps)
		{
			throw new NotImplementedException();
		}

		public Task Add(BaseRequestStep BaseRequestStep)
		{

			objectSet.Add(BaseRequestStep);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<BaseRequestStep> BaseRequestSteps)
		{
			objectSet.AddRange(BaseRequestSteps);
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
