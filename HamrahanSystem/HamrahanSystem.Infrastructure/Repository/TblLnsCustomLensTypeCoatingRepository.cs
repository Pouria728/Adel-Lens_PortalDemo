
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsCustomLensTypeCoatingRepository : EntityFrameworkRepository<TblLnsCustomLensTypeCoating>, ITblLnsCustomLensTypeCoatingRepository
    {
        public TblLnsCustomLensTypeCoatingRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsCustomLensTypeCoating> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblLnsCustomLensTypeCoating GetByKey(int _CustomLensTypeCoatingId)
        {
            return objectSet.SingleOrDefault(e => e.CustomLensTypeCoatingId == _CustomLensTypeCoatingId);
        }
        public Task<(List<TblLnsCustomLensTypeCoating>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
        {
            var countList = objectSet.Count();
            var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsCustomLensTypeCoating()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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
            objectSet.Where(e => e.CustomLensTypeCoatingId == _ID).ExecuteDelete();
            return Task.CompletedTask;
        }

        public Task Delete(List<TblLnsCustomLensTypeCoating> tblLnsCustomLensTypeCoatings)
        {
            objectSet.RemoveRange(tblLnsCustomLensTypeCoatings);

            return Task.CompletedTask;
        }

        public Task Update(TblLnsCustomLensTypeCoating tblLnsCustomLensTypeCoating)
        {
            var item = objectSet.Single(x => x.CustomLensTypeCoatingId == tblLnsCustomLensTypeCoating.CustomLensTypeCoatingId);
            item.DesignTypeId = tblLnsCustomLensTypeCoating.DesignTypeId;
            item.LensTypeName = tblLnsCustomLensTypeCoating.LensTypeName;
            item.CoatingName = tblLnsCustomLensTypeCoating.CoatingName;
            item.OrderId = tblLnsCustomLensTypeCoating.OrderId;
            item.IsActive = tblLnsCustomLensTypeCoating.IsActive;
            Save();
            return Task.CompletedTask;
        }


        public Task Update(List<TblLnsCustomLensTypeCoating> tblLnsCustomLensTypeCoatings)
        {
            throw new NotImplementedException();
        }

        public Task Add(TblLnsCustomLensTypeCoating tblLnsCustomLensTypeCoating)
        {

            objectSet.Add(tblLnsCustomLensTypeCoating);
            Save();
            return Task.CompletedTask;

        }

        public Task Add(List<TblLnsCustomLensTypeCoating> tblLnsCustomLensTypeCoatings)
        {
            objectSet.AddRange(tblLnsCustomLensTypeCoatings);
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

