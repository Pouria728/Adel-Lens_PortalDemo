
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsLensIndexRepository : EntityFrameworkRepository<TblLnsLensIndex>, ITblLnsLensIndexRepository
    {
        public TblLnsLensIndexRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsLensIndex> GetAll()
        {
            return objectSet.ToList();
        }
		public virtual Task<(List<TblLnsLensIndex>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Count();
			var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsLensIndex()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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

		public virtual TblLnsLensIndex GetByKey(int _LensIndexId)
        {
            return objectSet.SingleOrDefault(e => e.LensIndexId == _LensIndexId);
        }
		public Task Update(TblLnsLensIndex tblLnsLensIndex)
		{
			var item = objectSet.Single(x => x.LensIndexId == tblLnsLensIndex.LensIndexId);
			item.Name = tblLnsLensIndex.Name;
			item.IsActive = tblLnsLensIndex.IsActive;
			item.Code = tblLnsLensIndex.Code;
			item.Description = tblLnsLensIndex.Description;
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.LensIndexId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsLensIndex> tblLnsLensIndexs)
		{
			objectSet.RemoveRange(tblLnsLensIndexs);

			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsLensIndex> tblLnsLensIndexs)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblLnsLensIndex tblLnsLensIndex)
		{

			objectSet.Add(tblLnsLensIndex);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblLnsLensIndex> tblLnsLensIndexs)
		{
			objectSet.AddRange(tblLnsLensIndexs);
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
