
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsCustomDesignTypeAdditionRepository : EntityFrameworkRepository<TblLnsCustomDesignTypeAddition>, ITblLnsCustomDesignTypeAdditionRepository
    {
        public TblLnsCustomDesignTypeAdditionRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsCustomDesignTypeAddition> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblLnsCustomDesignTypeAddition GetByKey(int _CustomDesignTypeAdditionId)
        {
            return objectSet.SingleOrDefault(e => e.CustomDesignTypeAdditionId == _CustomDesignTypeAdditionId);
        }

        public Task<(List<TblLnsCustomDesignTypeAddition>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
        {
            var countList = objectSet.Count();
            var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsCustomDesignTypeAddition()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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
            objectSet.Where(e => e.CustomDesignTypeAdditionId == _ID).ExecuteDelete();
            return Task.CompletedTask;
        }

        public Task Delete(List<TblLnsCustomDesignTypeAddition> tblLnsCustomDesignTypeAdditions)
        {
            objectSet.RemoveRange(tblLnsCustomDesignTypeAdditions);
            return Task.CompletedTask;
        }

        public Task Update(TblLnsCustomDesignTypeAddition tblLnsCustomDesignTypeAddition)
        {
            var item = objectSet.Single(x => x.CustomDesignTypeAdditionId == tblLnsCustomDesignTypeAddition.CustomDesignTypeAdditionId);
            item.DesignTypeId = tblLnsCustomDesignTypeAddition.DesignTypeId;
            item.AdditionValue = tblLnsCustomDesignTypeAddition.AdditionValue;
            item.OrderId = tblLnsCustomDesignTypeAddition.OrderId;
            item.IsActive = tblLnsCustomDesignTypeAddition.IsActive;
            Save();
            return Task.CompletedTask;
        }

        public Task Update(List<TblLnsCustomDesignTypeAddition> tblLnsCustomDesignTypeAdditions)
        {
            throw new NotImplementedException();
        }

        public Task Add(TblLnsCustomDesignTypeAddition tblLnsCustomDesignTypeAddition)
        {
            objectSet.Add(tblLnsCustomDesignTypeAddition);
            Save();
            return Task.CompletedTask;
        }

        public Task Add(List<TblLnsCustomDesignTypeAddition> tblLnsCustomDesignTypeAdditions)
        {
            objectSet.AddRange(tblLnsCustomDesignTypeAdditions);
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
