
using HamrahanSystem.Domain.Entity;
using HamrahanSystem.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblWfwOrderProcessRepository : EntityFrameworkRepository<TblWfwOrderProcess>, ITblWfwOrderProcessRepository
    {
        public TblWfwOrderProcessRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblWfwOrderProcess> GetAll()
        {
            return objectSet.ToList();
        }
		public virtual Task<(List<TblWfwOrderProcess>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Count();
			var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblWfwOrderProcess()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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
		public virtual Task<(List<TblWfwOrderProcess>, int)> GetAllByFilter(int? OrderStatusId, int? IndexDocument, int? CustomerId, int? UserId, int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Where(x=>(OrderStatusId.HasValue==false|| x.StatusId==OrderStatusId)&& (IndexDocument.HasValue==false|| x.TblLnsOrder.IndexDocument==IndexDocument)
			&& (CustomerId.HasValue == false || x.TblLnsOrder.InfoCustomerId==CustomerId) && (UserId.HasValue == false ||x.TblLnsOrder.CreatedBy==UserId)).Count();
			

			var item = objectSet.Where(x => (OrderStatusId.HasValue == false || x.StatusId == OrderStatusId) && (IndexDocument.HasValue == false || x.TblLnsOrder.IndexDocument == IndexDocument)
			&& (CustomerId.HasValue == false || x.TblLnsOrder.InfoCustomerId == CustomerId) && (UserId.HasValue == false || x.TblLnsOrder.CreatedBy == UserId)).AsQueryable();
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

		public virtual TblWfwOrderProcess GetByKey(long _OrderId)
        {
            return objectSet.Include(x=>x.TblLnsOrder).SingleOrDefault(e => e.OrderProcessId == _OrderId);
        }
		public Task Update(TblWfwOrderProcess TblWfwOrderProcess)
		{
			var item = objectSet.Include(x=>x.TblLnsOrder).Single(x => x.OrderProcessId == TblWfwOrderProcess.OrderProcessId);
			item.DateComplete=TblWfwOrderProcess.DateComplete;
			item.StatusId=TblWfwOrderProcess.StatusId;
			item.TblLnsOrder.StatusId= TblWfwOrderProcess.TblLnsOrder.StatusId;
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.OrderProcessId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblWfwOrderProcess> TblWfwOrderProcesss)
		{
			objectSet.RemoveRange(TblWfwOrderProcesss);
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<TblWfwOrderProcess> TblWfwOrderProcesss)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblWfwOrderProcess TblWfwOrderProcess)
		{

			objectSet.Add(TblWfwOrderProcess);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblWfwOrderProcess> TblWfwOrderProcess)
		{
			objectSet.AddRange(TblWfwOrderProcess);
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
