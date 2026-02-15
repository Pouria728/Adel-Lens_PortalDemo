
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsDesignTypeLensIndexRepository : EntityFrameworkRepository<TblLnsDesignTypeLensIndex>, ITblLnsDesignTypeLensIndexRepository
    {
        public TblLnsDesignTypeLensIndexRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsDesignTypeLensIndex> GetAll()
        {
            return objectSet.ToList();
        }
		public virtual Task<(List<TblLnsDesignTypeLensIndex>, int)> GetAll(int brandDesignTypeId, int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Where(x => x.BrandDesignTypeId == brandDesignTypeId).Count();
			var item = objectSet.Where(x=>x.BrandDesignTypeId==brandDesignTypeId);
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsDesignTypeLensIndex()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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
        
            return Task.FromResult((item.Include(x=>x.TblLnsLensIndex)
				.Include(x => x.TblLnsBrandDesignType.TblLnsBrandLensType.TblLnsLensType)
				.Include(x => x.TblLnsBrandDesignType.TblLnsBrandLensType.TblLnsBrand)
				.Include(x => x.TblLnsBrandDesignType.TblLnsDesignType)
				.ToList(), countList));

		}

		public virtual TblLnsDesignTypeLensIndex GetByKey(int _DesignTypeLensIndexId)
        {
            return objectSet.Include(x => x.TblLnsLensIndex)
				.Include(x => x.TblLnsBrandDesignType.TblLnsBrandLensType.TblLnsLensType)
				.Include(x => x.TblLnsBrandDesignType.TblLnsBrandLensType.TblLnsBrand)
				.Include(x => x.TblLnsBrandDesignType.TblLnsDesignType).SingleOrDefault(e => e.DesignTypeLensIndexId == _DesignTypeLensIndexId);
        }
		public Task Update(TblLnsDesignTypeLensIndex tblLnsDesignTypeLensIndex)
		{
			var item = objectSet.Single(x => x.DesignTypeLensIndexId == tblLnsDesignTypeLensIndex.DesignTypeLensIndexId);
			item.LensIndexId = tblLnsDesignTypeLensIndex.LensIndexId;
			item.BrandDesignTypeId = tblLnsDesignTypeLensIndex.BrandDesignTypeId;
			item.OrderId = tblLnsDesignTypeLensIndex.OrderId;
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.DesignTypeLensIndexId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsDesignTypeLensIndex> tblLnsDesignTypeLensIndex)
		{
			objectSet.RemoveRange(tblLnsDesignTypeLensIndex);
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsDesignTypeLensIndex> tblLnsDesignTypeLensIndex)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblLnsDesignTypeLensIndex tblLnsDesignTypeLensIndex)
		{

			objectSet.Add(tblLnsDesignTypeLensIndex);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblLnsDesignTypeLensIndex> tblLnsDesignTypeLensIndex)
		{
			objectSet.AddRange(tblLnsDesignTypeLensIndex);
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
