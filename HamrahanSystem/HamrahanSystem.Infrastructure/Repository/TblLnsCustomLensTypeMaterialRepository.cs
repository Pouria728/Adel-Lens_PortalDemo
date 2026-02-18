
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsCustomLensTypeMaterialRepository : EntityFrameworkRepository<TblLnsCustomLensTypeMaterial>, ITblLnsCustomLensTypeMaterialRepository
    {
        public TblLnsCustomLensTypeMaterialRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsCustomLensTypeMaterial> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblLnsCustomLensTypeMaterial GetByKey(int _CustomLensTypeMaterialId)
        {
            return objectSet.SingleOrDefault(e => e.CustomLensTypeMaterialId == _CustomLensTypeMaterialId);
        }

        public Task<(List<TblLnsCustomLensTypeMaterial>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
        {
            var countList = objectSet.Count();
            var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsCustomLensTypeMaterial()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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

        public Task Delete(int _ID)
        {
            objectSet.Where(e => e.CustomLensTypeMaterialId == _ID).ExecuteDelete();
            return Task.CompletedTask;
        }

        public Task Delete(List<TblLnsCustomLensTypeMaterial> tblLnsCustomLensTypeMaterials)
        {
            objectSet.RemoveRange(tblLnsCustomLensTypeMaterials);
            return Task.CompletedTask;
        }

        public Task Update(TblLnsCustomLensTypeMaterial tblLnsCustomLensTypeMaterial)
        {
            var item = objectSet.Single(x => x.CustomLensTypeMaterialId == tblLnsCustomLensTypeMaterial.CustomLensTypeMaterialId);
            item.DesignTypeId = tblLnsCustomLensTypeMaterial.DesignTypeId;
            item.LensIndexId = tblLnsCustomLensTypeMaterial.LensIndexId;
            item.LensTypeName = tblLnsCustomLensTypeMaterial.LensTypeName;
            item.MaterialName = tblLnsCustomLensTypeMaterial.MaterialName;
            item.DefineObjectId = tblLnsCustomLensTypeMaterial.DefineObjectId;
            item.OrderId = tblLnsCustomLensTypeMaterial.OrderId;
            item.IsActive = tblLnsCustomLensTypeMaterial.IsActive;
            Save();
            return Task.CompletedTask;
        }

        public Task Update(List<TblLnsCustomLensTypeMaterial> tblLnsCustomLensTypeMaterials)
        {
            throw new NotImplementedException();
        }

        public Task Add(TblLnsCustomLensTypeMaterial tblLnsCustomLensTypeMaterial)
        {
            objectSet.Add(tblLnsCustomLensTypeMaterial);
            Save();
            return Task.CompletedTask;
        }

        public Task Add(List<TblLnsCustomLensTypeMaterial> tblLnsCustomLensTypeMaterials)
        {
            objectSet.AddRange(tblLnsCustomLensTypeMaterials);
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
