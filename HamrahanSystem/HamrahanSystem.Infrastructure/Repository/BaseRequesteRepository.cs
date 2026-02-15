
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class BaseRequesteRepository : EntityFrameworkRepository<BaseRequeste>, IBaseRequesteRepository
    {
        public BaseRequesteRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual IList<BaseRequeste> GetAll()
        {
            return objectSet.ToList();
        }
        public virtual Task<(List<BaseRequeste>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
        {
            var countList = objectSet.Count();
            var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new BaseRequeste()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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

        public virtual BaseRequeste GetByKey(int _BaseRequesteId)
        {
            return objectSet.SingleOrDefault(e => e.BaseRequestId == _BaseRequesteId);
        }
        public Task Update(BaseRequeste BaseRequeste)
        {
            var item = objectSet.Single(x => x.BaseRequestId == BaseRequeste.BaseRequestId);
            item.IsActive = BaseRequeste.IsActive;
            item.Title = BaseRequeste.Title;
            item.TypeRequest = BaseRequeste.TypeRequest;
            Save();
            return Task.CompletedTask;
        }
        public Task Delete(int _ID)
        {
            objectSet.Where(e => e.BaseRequestId == _ID).ExecuteDelete();
            return Task.CompletedTask;
        }

        public Task Delete(List<BaseRequeste> BaseRequestes)
        {
            objectSet.RemoveRange(BaseRequestes);
            Save();
            return Task.CompletedTask;
        }


        public Task Update(List<BaseRequeste> BaseRequestes)
        {
            throw new NotImplementedException();
        }

        public Task Add(BaseRequeste BaseRequeste)
        {
            BaseRequeste.DateCreate = DateTime.Now;

            objectSet.Add(BaseRequeste);
            Save();
            return Task.CompletedTask;

        }

        public Task Add(List<BaseRequeste> BaseRequestes)
        {
            objectSet.AddRange(BaseRequestes);
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
