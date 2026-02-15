
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsLensIndexMaterialTypeRepository : EntityFrameworkRepository<TblLnsLensIndexMaterialType>, ITblLnsLensIndexMaterialTypeRepository
    {
        public TblLnsLensIndexMaterialTypeRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsLensIndexMaterialType> GetAll()
        {
            return objectSet.ToList();
        }
		public virtual Task<(List<TblLnsLensIndexMaterialType>, int)> GetAll(int designTypeLensIndexId, int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Where(x => x.DesignTypeLensIndexId == designTypeLensIndexId).Count();
			var item = objectSet.Where(x=>x.DesignTypeLensIndexId==designTypeLensIndexId);
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsLensIndexMaterialType()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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
         
            return Task.FromResult((item.Include(x=>x.TblLnsMaterialType)
				.Include(x => x.TblLnsDesignTypeLensIndex.TblLnsLensIndex)
				.Include(x => x.TblLnsDesignTypeLensIndex.TblLnsBrandDesignType.TblLnsDesignType)
				.Include(x => x.TblLnsDesignTypeLensIndex.TblLnsBrandDesignType.TblLnsBrandLensType.TblLnsLensType)
				.Include(x => x.TblLnsDesignTypeLensIndex.TblLnsBrandDesignType.TblLnsBrandLensType.TblLnsBrand)
				.ToList(), countList));

		}

		public virtual TblLnsLensIndexMaterialType GetByKey(int _LensIndexMaterialTypeId)
        {
            return objectSet.Include(x => x.TblLnsMaterialType)
				.Include(x => x.TblLnsDesignTypeLensIndex.TblLnsLensIndex)
				.Include(x => x.TblLnsDesignTypeLensIndex.TblLnsBrandDesignType.TblLnsDesignType)
				.Include(x => x.TblLnsDesignTypeLensIndex.TblLnsBrandDesignType.TblLnsBrandLensType.TblLnsLensType)
				.Include(x => x.TblLnsDesignTypeLensIndex.TblLnsBrandDesignType.TblLnsBrandLensType.TblLnsBrand).SingleOrDefault(e => e.LensIndexMaterialTypeId == _LensIndexMaterialTypeId);
        }
		public Task Update(TblLnsLensIndexMaterialType tblLnsLensIndexMaterialType)
		{
			var item = objectSet.Single(x => x.LensIndexMaterialTypeId == tblLnsLensIndexMaterialType.LensIndexMaterialTypeId);
			item.DesignTypeLensIndexId = tblLnsLensIndexMaterialType.DesignTypeLensIndexId;
			item.MaterialTypeId = tblLnsLensIndexMaterialType.MaterialTypeId;
			item.DefineObjectId = tblLnsLensIndexMaterialType.DefineObjectId;
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.LensIndexMaterialTypeId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsLensIndexMaterialType> TblLnsBrands)
		{
			objectSet.RemoveRange(TblLnsBrands);
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsLensIndexMaterialType> tblLnsLensIndexMaterialTypes)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblLnsLensIndexMaterialType tblLnsLensIndexMaterialType)
		{

			objectSet.Add(tblLnsLensIndexMaterialType);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblLnsLensIndexMaterialType> tblLnsLensIndexMaterialTypes)
		{
			objectSet.AddRange(tblLnsLensIndexMaterialTypes);
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
