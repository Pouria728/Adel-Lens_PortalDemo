
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsLensTypeRLensIndexRRepository : EntityFrameworkRepository<TblLnsLensTypeRLensIndexR>, ITblLnsLensTypeRLensIndexRRepository
    {
        public TblLnsLensTypeRLensIndexRRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsLensTypeRLensIndexR> GetAll()
        {
            return objectSet.Include(x => x.TblLnsLensIndexR).OrderBy(x => x.OrderId).ToList();
        }
		public virtual Task<(List<TblLnsLensTypeRLensIndexR>, int)> GetAll(int brandLensTypeRId,int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Where(x => x.BrandLensTypeRId == brandLensTypeRId).Count();
			var item = objectSet.Where(x=>x.BrandLensTypeRId== brandLensTypeRId);
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsLensTypeRLensIndexR()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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
        
            return Task.FromResult((item.Include(x=>x.TblLnsLensIndexR).Include(x=>x.TblLnsBrandLensTypeR).Include(x=>x.TblLnsBrandLensTypeR.TblLnsLensTypeR).Include(x=>x.TblLnsBrandLensTypeR.TblLnsBrand).OrderBy(x => x.OrderId).ToList(), countList));

		}

		public virtual TblLnsLensTypeRLensIndexR GetByKey(int _LensTypeRLensIndexRId)
        {
            return objectSet.Include(x => x.TblLnsLensIndexR).Include(x => x.TblLnsBrandLensTypeR).Include(x => x.TblLnsBrandLensTypeR.TblLnsLensTypeR).Include(x => x.TblLnsBrandLensTypeR.TblLnsBrand).SingleOrDefault(e => e.LensTypeRLensIndexRId == _LensTypeRLensIndexRId);
        }
		public Task Update(TblLnsLensTypeRLensIndexR tblLnsLensTypeRLensIndexR)
		{
			var item = objectSet.Single(x => x.LensTypeRLensIndexRId == tblLnsLensTypeRLensIndexR.LensTypeRLensIndexRId);
			item.BrandLensTypeRId = tblLnsLensTypeRLensIndexR.BrandLensTypeRId;
			item.LensIndexRId = tblLnsLensTypeRLensIndexR.LensIndexRId;
			item.OrderId = tblLnsLensTypeRLensIndexR.OrderId;
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.LensTypeRLensIndexRId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsLensTypeRLensIndexR> tblLnsLensTypeRLensIndexRs)
		{
			objectSet.RemoveRange(tblLnsLensTypeRLensIndexRs);
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsLensTypeRLensIndexR> tblLnsLensTypeRLensIndexRs)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblLnsLensTypeRLensIndexR tblLnsLensTypeRLensIndexR)
		{

			objectSet.Add(tblLnsLensTypeRLensIndexR);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblLnsLensTypeRLensIndexR> tblLnsLensTypeRLensIndexRs)
		{
			objectSet.AddRange(tblLnsLensTypeRLensIndexRs);
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
