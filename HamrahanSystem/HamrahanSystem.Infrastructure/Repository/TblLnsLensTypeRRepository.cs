
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsLensTypeRRepository : EntityFrameworkRepository<TblLnsLensTypeR>, ITblLnsLensTypeRRepository
    {
        public TblLnsLensTypeRRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsLensTypeR> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblLnsLensTypeR GetByKey(int _LensTypeRId)
        {
            return objectSet.SingleOrDefault(e => e.LensTypeRId == _LensTypeRId);
        }
		public Task<(List<TblLnsLensTypeR>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Count();
			var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsLensTypeR()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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
			objectSet.Where(e => e.LensTypeRId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsLensTypeR> tblLnsLensTypeRs)
		{
			objectSet.RemoveRange(tblLnsLensTypeRs);

			return Task.CompletedTask;
		}

		public Task Update(TblLnsLensTypeR tblLnsLensTypeR)
		{
			var item = objectSet.Single(x => x.LensTypeRId == tblLnsLensTypeR.LensTypeRId);
			item.Code = tblLnsLensTypeR.Code;
			item.IsActive = tblLnsLensTypeR.IsActive;
			item.Name = tblLnsLensTypeR.Name;
			item.OrderId = tblLnsLensTypeR.OrderId;
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsLensTypeR> tblLnsLensTypes)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblLnsLensTypeR tblLnsLensTypeR)
		{

			objectSet.Add(tblLnsLensTypeR);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblLnsLensTypeR> tblLnsLensTypeRs)
		{
			objectSet.AddRange(tblLnsLensTypeRs);
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
