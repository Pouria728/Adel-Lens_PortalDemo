
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsLensIndexRSphRepository : EntityFrameworkRepository<TblLnsLensIndexRSph>, ITblLnsLensIndexRSphRepository
    {
        public TblLnsLensIndexRSphRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsLensIndexRSph> GetAll()
        {
            return objectSet.Include(x => x.TblLnsSph).OrderBy(x => x.OrderId).ToList();
        }
		public virtual Task<(List<TblLnsLensIndexRSph>, int)> GetAll(int lensTypeRLensIndexRId,int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Where(x => x.LensTypeRLensIndexRId == lensTypeRLensIndexRId).Count();
			var item = objectSet.Where(x=>x.LensTypeRLensIndexRId== lensTypeRLensIndexRId);
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsLensIndexRSph()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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
        
            return Task.FromResult((item.Include(x=>x.TblLnsLensTypeRLensIndexR)
				.Include(x=>x.TblLnsSph)
				.Include(x=>x.TblLnsLensTypeRLensIndexR.TblLnsBrandLensTypeR.TblLnsLensTypeR)
				.Include(x => x.TblLnsLensTypeRLensIndexR.TblLnsBrandLensTypeR.TblLnsBrand)
				.Include(x=>x.TblLnsLensTypeRLensIndexR.TblLnsLensIndexR).OrderBy(x => x.OrderId).ToList(), countList));

		}

		public virtual TblLnsLensIndexRSph GetByKey(int _LensIndexRSphId)
        {
            return objectSet.Include(x => x.TblLnsLensTypeRLensIndexR)
				.Include(x => x.TblLnsSph)
				.Include(x => x.TblLnsLensTypeRLensIndexR.TblLnsBrandLensTypeR.TblLnsLensTypeR)
				.Include(x => x.TblLnsLensTypeRLensIndexR.TblLnsBrandLensTypeR.TblLnsBrand)
				.Include(x => x.TblLnsLensTypeRLensIndexR.TblLnsLensIndexR).SingleOrDefault(e => e.LensIndexRSphId == _LensIndexRSphId);
        }
		public Task Update(TblLnsLensIndexRSph tblLnsLensIndexRSph)
		{
			var item = objectSet.Single(x => x.LensIndexRSphId == tblLnsLensIndexRSph.LensIndexRSphId);
			item.LensTypeRLensIndexRId = tblLnsLensIndexRSph.LensTypeRLensIndexRId;
			item.SphId = tblLnsLensIndexRSph.SphId;
			item.OrderId = tblLnsLensIndexRSph.OrderId;
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.LensIndexRSphId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsLensIndexRSph> TblLnsBrands)
		{
			objectSet.RemoveRange(TblLnsBrands);
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsLensIndexRSph> tblLnsLensIndexRSphs)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblLnsLensIndexRSph tblLnsLensIndexRSph)
		{

			objectSet.Add(tblLnsLensIndexRSph);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblLnsLensIndexRSph> tblLnsLensIndexRSphs)
		{
			objectSet.AddRange(tblLnsLensIndexRSphs);
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
