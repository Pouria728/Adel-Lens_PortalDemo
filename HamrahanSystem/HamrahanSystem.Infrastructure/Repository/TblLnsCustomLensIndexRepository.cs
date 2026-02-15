
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsCustomLensIndexRepository : EntityFrameworkRepository<TblLnsCustomLensIndex>, ITblLnsCustomLensIndexRepository
    {
        public TblLnsCustomLensIndexRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsCustomLensIndex> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblLnsCustomLensIndex GetByKey(int _CustomLensIndexId)
        {
            return objectSet.SingleOrDefault(e => e.CustomLensIndexId == _CustomLensIndexId);
        }
        public Task<(List<TblLnsCustomLensIndex>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
        {
            var countList = objectSet.Count();
            var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsCustomLensIndex()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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
            objectSet.Where(e => e.CustomLensIndexId == _ID).ExecuteDelete();
            return Task.CompletedTask;
        }

        public Task Delete(List<TblLnsCustomLensIndex> tblLnsCustomLensIndices)
        {
            objectSet.RemoveRange(tblLnsCustomLensIndices);

            return Task.CompletedTask;
        }

        public Task Update(TblLnsCustomLensIndex tblLnsCustomLensIndex)
        {
            var item = objectSet.Single(x => x.CustomLensIndexId == tblLnsCustomLensIndex.CustomLensIndexId);
            item.DesignTypeId = tblLnsCustomLensIndex.DesignTypeId;
            item.LensIndexName = tblLnsCustomLensIndex.LensIndexName;
            item.ColoringTypeStatus = tblLnsCustomLensIndex.ColoringTypeStatus;
            item.OrderId = tblLnsCustomLensIndex.OrderId;
            item.IsActive = tblLnsCustomLensIndex.IsActive;
            Save();
            return Task.CompletedTask;
        }


        public Task Update(List<TblLnsCustomLensIndex> tblLnsCustomLensIndices)
        {
            throw new NotImplementedException();
        }

        public Task Add(TblLnsCustomLensIndex tblLnsCustomLensIndex)
        {

            objectSet.Add(tblLnsCustomLensIndex);
            Save();
            return Task.CompletedTask;

        }

        public Task Add(List<TblLnsCustomLensIndex> tblLnsCustomLensIndices)
        {
            objectSet.AddRange(tblLnsCustomLensIndices);
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
