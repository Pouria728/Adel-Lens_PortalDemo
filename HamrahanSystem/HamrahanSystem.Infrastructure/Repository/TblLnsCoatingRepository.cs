
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsCoatingRepository : EntityFrameworkRepository<TblLnsCoating>, ITblLnsCoatingRepository
    {
        public TblLnsCoatingRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsCoating> GetAll()
        {
            return objectSet.ToList();
        }
		public virtual Task<(List<TblLnsCoating>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Count();
			var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsCoating()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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

		public virtual TblLnsCoating GetByKey(int _CoatingId)
        {
            return objectSet.SingleOrDefault(e => e.CoatingId == _CoatingId);
        }

		public Task Update(TblLnsCoating tblLnsCoating)
		{
			var item = objectSet.Single(x => x.CoatingId == tblLnsCoating.CoatingId);
			item.Code = tblLnsCoating.Code;
			item.IsActive = tblLnsCoating.IsActive;
			item.Name = tblLnsCoating.Name;
			item.Description = tblLnsCoating.Description;
			item.OrderId = tblLnsCoating.OrderId;

			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.CoatingId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsCoating> tblLnsCoatings)
		{
			objectSet.RemoveRange(tblLnsCoatings);

			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsCoating> taxPayerCustomers)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblLnsCoating tblLnsCoating)
		{

			objectSet.Add(tblLnsCoating);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblLnsCoating> tblLnsCoatings)
		{
			objectSet.AddRange(tblLnsCoatings);
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
