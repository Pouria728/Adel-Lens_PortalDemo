
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsSphRepository : EntityFrameworkRepository<TblLnsSph>, ITblLnsSphRepository
    {
        public TblLnsSphRepository(AdelModel context)
            : base(context)
        {
        }

		private void EnsureUniqueOrderId(TblLnsSph tblLnsSph)
		{
			var exists = objectSet.Any(x => x.OrderId == tblLnsSph.OrderId
				&& x.SphId != tblLnsSph.SphId);
			if (exists)
				throw new InvalidOperationException("ترتیب نمایش تکراری است.");
		}

        public virtual ICollection<TblLnsSph> GetAll()
        {
            return objectSet.OrderBy(x => x.OrderId).ThenBy(x => x.SphId).ToList();
        }
		public virtual Task<(List<TblLnsSph>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Count();
			var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsSph()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
                if (colsort != null)
                    item = sort == "asc" ? item.OrderBy(x => EF.Property<object>(x, colsort)) : item.OrderByDescending(x => EF.Property<object>(x, colsort));
                else
                    item = item.OrderBy(x => x.OrderId).ThenBy(x => x.SphId);
            }
            else
            {
                item = item.OrderBy(x => x.OrderId).ThenBy(x => x.SphId);
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

		public virtual TblLnsSph GetByKey(int _SphId)
        {
            return objectSet.SingleOrDefault(e => e.SphId == _SphId);
        }
		public Task Update(TblLnsSph tblLnsSph)
		{
			EnsureUniqueOrderId(tblLnsSph);
			var item = objectSet.Single(x => x.SphId == tblLnsSph.SphId);
			item.Name = tblLnsSph.Name;
			item.IsActive = tblLnsSph.IsActive;
			item.Code = tblLnsSph.Code;
			item.Description = tblLnsSph.Description;
			item.OrderId = tblLnsSph.OrderId;
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.SphId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsSph> tblLnsSphes)
		{
			objectSet.RemoveRange(tblLnsSphes);
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsSph> tblLnsSphes)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblLnsSph tblLnsSph)
		{
			EnsureUniqueOrderId(tblLnsSph);
			objectSet.Add(tblLnsSph);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblLnsSph> tblLnsSphes)
		{
			objectSet.AddRange(tblLnsSphes);
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
