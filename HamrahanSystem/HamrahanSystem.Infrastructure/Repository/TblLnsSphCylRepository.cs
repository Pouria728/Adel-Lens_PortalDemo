
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsSphCylRepository : EntityFrameworkRepository<TblLnsSphCyl>, ITblLnsSphCylRepository
    {
        public TblLnsSphCylRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsSphCyl> GetAll()
        {
            return objectSet.Include(x => x.TblLnsCyl).Include(x => x.TblClrDefineObject).OrderBy(x => x.OrderId).ToList();
        }
		public virtual Task<(List<TblLnsSphCyl>, int)> GetAll(int lensIndexRSphId,int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Where(x => x.LensIndexRSphId == lensIndexRSphId).Count();
			var item = objectSet.Where(x=>x.LensIndexRSphId==lensIndexRSphId);
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsSphCyl()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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
       
            return Task.FromResult((item.Include(x=>x.TblLnsLensIndexRSph.TblLnsSph)
				.Include(x => x.TblLnsLensIndexRSph.TblLnsSph)
				.Include(x=>x.TblLnsCyl)
				.Include(x => x.TblLnsLensIndexRSph.TblLnsLensTypeRLensIndexR.TblLnsLensIndexR)
				.Include(x => x.TblLnsLensIndexRSph.TblLnsLensTypeRLensIndexR.TblLnsBrandLensTypeR.TblLnsLensTypeR)
				.Include(x => x.TblLnsLensIndexRSph.TblLnsLensTypeRLensIndexR.TblLnsBrandLensTypeR.TblLnsBrand).OrderBy(x => x.OrderId)
                .ToList(), countList));

		}

		public virtual TblLnsSphCyl GetByKey(int _SphCylId)
        {
            return objectSet.Include(x => x.TblLnsLensIndexRSph.TblLnsSph)
				.Include(x => x.TblLnsLensIndexRSph.TblLnsSph)
				.Include(x => x.TblLnsCyl)
				.Include(x => x.TblLnsLensIndexRSph.TblLnsLensTypeRLensIndexR.TblLnsLensIndexR)
				.Include(x => x.TblLnsLensIndexRSph.TblLnsLensTypeRLensIndexR.TblLnsBrandLensTypeR.TblLnsLensTypeR)
				.Include(x => x.TblLnsLensIndexRSph.TblLnsLensTypeRLensIndexR.TblLnsBrandLensTypeR.TblLnsBrand).SingleOrDefault(e => e.SphCylId == _SphCylId);
        }
		public Task Update(TblLnsSphCyl tblLnsBrand)
		{
			var item = objectSet.Single(x => x.SphCylId == tblLnsBrand.SphCylId);
			item.LensIndexRSphId = tblLnsBrand.LensIndexRSphId;
			item.DefineObjectId = tblLnsBrand.DefineObjectId;
			item.CylId = tblLnsBrand.CylId;
			item.OrderId = tblLnsBrand.OrderId;
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.SphCylId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsSphCyl> tblLnsSphCyls)
		{
			objectSet.RemoveRange(tblLnsSphCyls);
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsSphCyl> tblLnsSphCyls)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblLnsSphCyl tblLnsSphCyl)
		{

			objectSet.Add(tblLnsSphCyl);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblLnsSphCyl> tblLnsSphCyls)
		{
			objectSet.AddRange(tblLnsSphCyls);
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
