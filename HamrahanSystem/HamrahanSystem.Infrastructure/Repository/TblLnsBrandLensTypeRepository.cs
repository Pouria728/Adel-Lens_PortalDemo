
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsBrandLensTypeRepository : EntityFrameworkRepository<TblLnsBrandLensType>, ITblLnsBrandLensTypeRepository
    {
        public TblLnsBrandLensTypeRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsBrandLensType> GetAll()
        {
            return objectSet.ToList();
        }
		public virtual Task<(List<TblLnsBrandLensType>, int)> GetAll(int BrandId,int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Where(x => x.BrandId == BrandId).Count();
			var item = objectSet.Where(x=>x.BrandId==BrandId);
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsBrandLensType()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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
    
            return Task.FromResult((item.Include(x=>x.TblLnsBrand)
				.Include(x=>x.TblLnsLensType)
				.ToList(), countList));

		}

		public virtual TblLnsBrandLensType GetByKey(int _BrandLensTypeId)
        {
            return objectSet.Include(x => x.TblLnsBrand).Include(x => x.TblLnsLensType).SingleOrDefault(e => e.BrandLensTypeId == _BrandLensTypeId);
        }
		public Task Update(TblLnsBrandLensType tblLnsBrandLensType)
		{
			var item = objectSet.Single(x => x.BrandLensTypeId == tblLnsBrandLensType.BrandId);
			item.BrandId = tblLnsBrandLensType.BrandId;
			item.LensTypeId = tblLnsBrandLensType.LensTypeId;
			item.OrderId = tblLnsBrandLensType.OrderId;
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.BrandId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsBrandLensType> tblLnsBrandLensTypes)
		{
			objectSet.RemoveRange(tblLnsBrandLensTypes);
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsBrandLensType> tblLnsBrandLensTypes)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblLnsBrandLensType tblLnsBrandLensType)
		{

			objectSet.Add(tblLnsBrandLensType);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblLnsBrandLensType> tblLnsBrandLensTypes)
		{
			objectSet.AddRange(tblLnsBrandLensTypes);
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
