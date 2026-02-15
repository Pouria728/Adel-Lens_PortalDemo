using System;
using System.Linq;
using HamrahanSystem.Domain.Entity;
using HamrahanSystem.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsCustomCylRepository : EntityFrameworkRepository<TblLnsCustomCyl>, ITblLnsCustomCylRepository
    {
        public TblLnsCustomCylRepository(AdelModel context) : base(context)
        {
        }

        public virtual ICollection<TblLnsCustomCyl> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblLnsCustomCyl GetByKey(int customCylId)
        {
            return objectSet.SingleOrDefault(e => e.CustomCylId == customCylId);
        }

        public Task<(List<TblLnsCustomCyl>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
        {
            var countList = objectSet.Count();
            var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsCustomCyl()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
                if (colsort != null)
                {
                    item = sort == "asc" ? item.OrderBy(x => EF.Property<object>(x, colsort)) : item.OrderByDescending(x => EF.Property<object>(x, colsort));
                }
            }

            if (maxResult.HasValue && maxResult > 0)
            {
                item = item.Take(maxResult.Value);
            }

            if (page.HasValue && rowInPage.HasValue && countList > 0)
            {
                int totalPages = (int)Math.Ceiling((float)countList / (float)rowInPage);
                page = page.Value > totalPages ? totalPages : page.Value;
                int countIndexes = ((page.Value - 1) * rowInPage.Value) + rowInPage.Value > countList ? countList - ((page.Value - 1) * rowInPage.Value) : rowInPage.Value;
                item = item.Skip((page.Value - 1) * rowInPage.Value).Take(countIndexes);
            }

            return Task.FromResult((item.ToList(), countList));
        }

        public Task Delete(int id)
        {
            objectSet.Where(e => e.CustomCylId == id).ExecuteDelete();
            return Task.CompletedTask;
        }

        public Task Delete(List<TblLnsCustomCyl> items)
        {
            objectSet.RemoveRange(items);
            return Task.CompletedTask;
        }

        public Task Update(TblLnsCustomCyl item)
        {
            var oldItem = objectSet.Single(x => x.CustomCylId == item.CustomCylId);
            oldItem.Name = item.Name;
            oldItem.Code = item.Code;
            oldItem.Description = item.Description;
            oldItem.OrderId = item.OrderId;
            oldItem.IsActive = item.IsActive;
            Save();
            return Task.CompletedTask;
        }

        public Task Update(List<TblLnsCustomCyl> items)
        {
            throw new NotImplementedException();
        }

        public Task Add(TblLnsCustomCyl item)
        {
            objectSet.Add(item);
            Save();
            return Task.CompletedTask;
        }

        public Task Add(List<TblLnsCustomCyl> items)
        {
            objectSet.AddRange(items);
            return Task.CompletedTask;
        }

        public new AdelModel Context
        {
            get { return (AdelModel)base.Context; }
        }
    }
}
