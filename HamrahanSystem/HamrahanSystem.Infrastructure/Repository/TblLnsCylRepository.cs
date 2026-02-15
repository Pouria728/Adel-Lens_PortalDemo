
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsCylRepository : EntityFrameworkRepository<TblLnsCyl>, ITblLnsCylRepository
    {
        public TblLnsCylRepository(AdelModel context)
            : base(context)
        {
        }

		private void EnsureUniqueOrderId(TblLnsCyl tblLnsCyl)
		{
			var exists = objectSet.Any(x => x.OrderId == tblLnsCyl.OrderId
				&& x.CylId != tblLnsCyl.CylId);
			if (exists)
				throw new InvalidOperationException("ترتیب نمایش تکراری است.");
		}

        public virtual ICollection<TblLnsCyl> GetAll()
        {
            return objectSet.OrderBy(x => x.OrderId).ThenBy(x => x.CylId).ToList();
        }
		public virtual Task<(List<TblLnsCyl>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Count();
			var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsCyl()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
                if (colsort != null)
                    item = sort == "asc" ? item.OrderBy(x => EF.Property<object>(x, colsort)) : item.OrderByDescending(x => EF.Property<object>(x, colsort));
                else
                    item = item.OrderBy(x => x.OrderId).ThenBy(x => x.CylId);
            }
            else
            {
                item = item.OrderBy(x => x.OrderId).ThenBy(x => x.CylId);
            }
            if (page.HasValue && rowInPage.HasValue && countList > 0)
            {
                int totalPages = (int)Math.Ceiling((float)countList / (float)rowInPage);
                page = page.Value > totalPages ? totalPages : page.Value;
                int CountIndexs = ((page.Value - 1) * rowInPage.Value) + rowInPage.Value > countList ? countList - ((page.Value - 1) * rowInPage.Value) : rowInPage.Value;
                item = item.Skip((page.Value - 1) * rowInPage.Value).Take(CountIndexs);
            }
            if (maxResult.HasValue && maxResult > 0)
			{
				item = item.Take(maxResult.Value);
			}
      
        
       
            return Task.FromResult((item.ToList(), countList));

		}

		public virtual TblLnsCyl GetByKey(int _CylId)
        {
            return objectSet.SingleOrDefault(e => e.CylId == _CylId);
        }
		public Task Update(TblLnsCyl tblLnsCyl)
		{
			EnsureUniqueOrderId(tblLnsCyl);
			var item = objectSet.Single(x => x.CylId == tblLnsCyl.CylId);
			item.Name = tblLnsCyl.Name;
			item.IsActive = tblLnsCyl.IsActive;
			item.Code = tblLnsCyl.Code;
			item.Description = tblLnsCyl.Description;
			item.OrderId = tblLnsCyl.OrderId;
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.CylId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsCyl> tblLnsCyles)
		{
			objectSet.RemoveRange(tblLnsCyles);

			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsCyl> tblLnsCyles)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblLnsCyl tblLnsCyl)
		{
			EnsureUniqueOrderId(tblLnsCyl);
			objectSet.Add(tblLnsCyl);
			Save();
			return Task.CompletedTask;

		}
		public Task Add(List<TblLnsCyl> TblLnsCyles)
		{
			objectSet.AddRange(TblLnsCyles);
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
