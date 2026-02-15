using System;
using System.Linq;
using HamrahanSystem.Domain.Entity;
using HamrahanSystem.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsCustomSphRepository : EntityFrameworkRepository<TblLnsCustomSph>, ITblLnsCustomSphRepository
    {
        public TblLnsCustomSphRepository(AdelModel context) : base(context)
        {
        }

        public virtual ICollection<TblLnsCustomSph> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblLnsCustomSph GetByKey(int customSphId)
        {
            return objectSet.SingleOrDefault(e => e.CustomSphId == customSphId);
        }

        public Task<(List<TblLnsCustomSph>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
        {
            var countList = objectSet.Count();
            var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsCustomSph()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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
            objectSet.Where(e => e.CustomSphId == id).ExecuteDelete();
            return Task.CompletedTask;
        }

        public Task Delete(List<TblLnsCustomSph> items)
        {
            objectSet.RemoveRange(items);
            return Task.CompletedTask;
        }

        public Task Update(TblLnsCustomSph item)
        {
            var oldItem = objectSet.Single(x => x.CustomSphId == item.CustomSphId);
            oldItem.Name = item.Name;
            oldItem.Code = item.Code;
            oldItem.Description = item.Description;
            oldItem.OrderId = item.OrderId;
            oldItem.IsActive = item.IsActive;
            Save();
            return Task.CompletedTask;
        }

        public Task Update(List<TblLnsCustomSph> items)
        {
            throw new NotImplementedException();
        }

        public Task Add(TblLnsCustomSph item)
        {
            objectSet.Add(item);
            Save();
            return Task.CompletedTask;
        }

        public Task Add(List<TblLnsCustomSph> items)
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
