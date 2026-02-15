
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;


namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsBrandLensTypeRRepository : EntityFrameworkRepository<TblLnsBrandLensTypeR>, ITblLnsBrandLensTypeRRepository
    {
        public TblLnsBrandLensTypeRRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsBrandLensTypeR> GetAll()
        {
            return objectSet.Include(x => x.TblLnsLensTypeR).OrderBy(x => x.OrderId).ToList();
        }
		public virtual Task<(List<TblLnsBrandLensTypeR>, int)> GetAll(int BrandId, int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Where(x => x.BrandId == BrandId).Count();
			var item = objectSet.Where(x=>x.BrandId==BrandId);
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsBrandLensTypeR()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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
       
            return Task.FromResult((item.Include(x => x.TblLnsBrand).Include(x=>x.TblLnsLensTypeR).OrderBy(x => x.OrderId).ToList(), countList));

		}

		public virtual TblLnsBrandLensTypeR GetByKey(int _BrandLensTypeRId)
        {
            return objectSet.Include(x => x.TblLnsBrand).Include(x => x.TblLnsLensTypeR).SingleOrDefault(e => e.BrandLensTypeRId == _BrandLensTypeRId);
        }
		public Task Update(TblLnsBrandLensTypeR tblLnsBrandLensTypeR)
		{
			var item = objectSet.Single(x => x.BrandLensTypeRId == tblLnsBrandLensTypeR.BrandLensTypeRId);
			item.BrandId = tblLnsBrandLensTypeR.BrandId;
			item.LensTypeRId = tblLnsBrandLensTypeR.LensTypeRId;
			item.OrderId = tblLnsBrandLensTypeR.OrderId;
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.BrandId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsBrandLensTypeR> tblLnsBrandLensTypeR)
		{
			objectSet.RemoveRange(tblLnsBrandLensTypeR);

			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsBrandLensTypeR> tblLnsBrandLensTypeR)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblLnsBrandLensTypeR tblLnsBrandLensTypeR)
		{

			objectSet.Add(tblLnsBrandLensTypeR);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblLnsBrandLensTypeR> tblLnsBrandLensTypeR)
		{
			objectSet.AddRange(tblLnsBrandLensTypeR);
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
