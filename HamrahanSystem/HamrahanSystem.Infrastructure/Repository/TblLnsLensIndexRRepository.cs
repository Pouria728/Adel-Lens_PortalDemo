
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsLensIndexRRepository : EntityFrameworkRepository<TblLnsLensIndexR>, ITblLnsLensIndexRRepository
    {
        public TblLnsLensIndexRRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsLensIndexR> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblLnsLensIndexR GetByKey(int _LensIndexRId)
        {
            return objectSet.SingleOrDefault(e => e.LensIndexRId == _LensIndexRId);
        }
		public virtual Task<(List<TblLnsLensIndexR>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Count();
			var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsLensIndexR()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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
		public Task Update(TblLnsLensIndexR tblLnsLensIndexR)
		{
			var item = objectSet.Single(x => x.LensIndexRId == tblLnsLensIndexR.LensIndexRId);
			item.Name = tblLnsLensIndexR.Name;
			item.IsActive = tblLnsLensIndexR.IsActive;
			item.Code = tblLnsLensIndexR.Code;
			item.Description = tblLnsLensIndexR.Description;
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.LensIndexRId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsLensIndexR> tblLnsLensIndexRs)
		{
			objectSet.RemoveRange(tblLnsLensIndexRs);

			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsLensIndexR> tblLnsLensIndexRs)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblLnsLensIndexR tblLnsLensIndexR)
		{

			objectSet.Add(tblLnsLensIndexR);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblLnsLensIndexR> tblLnsLensIndexRs)
		{
			objectSet.AddRange(tblLnsLensIndexRs);
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
