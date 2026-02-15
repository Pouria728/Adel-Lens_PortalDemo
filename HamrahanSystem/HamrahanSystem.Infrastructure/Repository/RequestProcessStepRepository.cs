
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class RequestProcessStepRepository : EntityFrameworkRepository<RequestProcessStep>, IRequestProcessStepRepository
    {
        public RequestProcessStepRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<RequestProcessStep> GetAll()
        {
            return objectSet.ToList();
        }
		public virtual Task<(List<RequestProcessStep>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Count();
			var item = objectSet.AsQueryable();
			if (!string.IsNullOrEmpty(sidx))
			{
				var colsort = (new RequestProcessStep()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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

		public virtual RequestProcessStep GetByKey(Guid _RequestProcessStepId)
        {
            return objectSet.SingleOrDefault(e => e.RequestProcessStepId == _RequestProcessStepId);
        }
		public Task Update(RequestProcessStep RequestProcessStep)
		{
			var item = objectSet.Single(x => x.RequestProcessStepId == RequestProcessStep.RequestProcessStepId);
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(Guid _ID)
		{
			objectSet.Where(e => e.RequestProcessStepId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<RequestProcessStep> RequestProcessSteps)
		{
			objectSet.RemoveRange(RequestProcessSteps);
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<RequestProcessStep> RequestProcessSteps)
		{
			throw new NotImplementedException();
		}

		public Task Add(RequestProcessStep RequestProcessStep)
		{

			objectSet.Add(RequestProcessStep);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<RequestProcessStep> RequestProcessSteps)
		{
			objectSet.AddRange(RequestProcessSteps);
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
