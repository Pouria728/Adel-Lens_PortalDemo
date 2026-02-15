
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsBrandDesignTypeRepository : EntityFrameworkRepository<TblLnsBrandDesignType>, ITblLnsBrandDesignTypeRepository
    {
        public TblLnsBrandDesignTypeRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsBrandDesignType> GetAll()
        {
            return objectSet.ToList();
        }
		public virtual Task<(List<TblLnsBrandDesignType>, int)> GetAll(int brandLensTypeId, int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Where(x => x.BrandLensTypeId == brandLensTypeId).Count();
			var item = objectSet.Where(x=>x.BrandLensTypeId==brandLensTypeId);
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsBrandDesignType()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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
    
            return Task.FromResult((item.Include(x=>x.TblLnsBrandLensType.TblLnsLensType)
				.Include(x => x.TblLnsBrandLensType.TblLnsBrand)
				.Include(x => x.TblLnsDesignType)
				.ToList(), countList));

		}

		public virtual TblLnsBrandDesignType GetByKey(int _BrandDesignTypeId)
        {
            return objectSet.Include(x => x.TblLnsBrandLensType.TblLnsLensType)
				.Include(x => x.TblLnsBrandLensType.TblLnsBrand)
				.Include(x => x.TblLnsDesignType).SingleOrDefault(e => e.BrandDesignTypeId == _BrandDesignTypeId);
        }
		public Task Update(TblLnsBrandDesignType tblLnsBrandDesignType)
		{
			var item = objectSet.Single(x => x.BrandDesignTypeId == tblLnsBrandDesignType.BrandDesignTypeId);
			item.BrandLensTypeId = tblLnsBrandDesignType.BrandLensTypeId;
			item.DesignTypeId = tblLnsBrandDesignType.DesignTypeId;
			item.OrderId = tblLnsBrandDesignType.OrderId;
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.BrandDesignTypeId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsBrandDesignType> tblLnsBrandDesignTypes)
		{
			objectSet.RemoveRange(tblLnsBrandDesignTypes);
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsBrandDesignType> tblLnsBrandDesignTypes)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblLnsBrandDesignType tblLnsBrandDesignType)
		{

			objectSet.Add(tblLnsBrandDesignType);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblLnsBrandDesignType> tblLnsBrandDesignTypes)
		{
			objectSet.AddRange(tblLnsBrandDesignTypes);
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
