
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsLensTypeRepository : EntityFrameworkRepository<TblLnsLensType>, ITblLnsLensTypeRepository
    {
        public TblLnsLensTypeRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsLensType> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblLnsLensType GetByKey(int _LensTypeId)
        {
            return objectSet.SingleOrDefault(e => e.LensTypeId == _LensTypeId);
        }

		public Task<(List<TblLnsLensType>, int)> GetAll( int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Count();
			var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsLensType()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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
			objectSet.Where(e => e.LensTypeId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsLensType> tblLnsLensTypes)
		{
			objectSet.RemoveRange(tblLnsLensTypes);

			return Task.CompletedTask;
		}

		public Task Update(TblLnsLensType tblLnsLensType)
		{
			var item = objectSet.Single(x => x.LensTypeId == tblLnsLensType.LensTypeId);
			item.Code = tblLnsLensType.Code;
			item.BrandId = tblLnsLensType.BrandId;
			item.IsActive = tblLnsLensType.IsActive;
			item.IsCorridor = tblLnsLensType.IsCorridor;
			item.Description = tblLnsLensType.Description;
			item.IsSpecial = tblLnsLensType.IsSpecial;
			item.Name = tblLnsLensType.Name;
			item.OrderId = tblLnsLensType.OrderId;
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsLensType> tblLnsLensTypes)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblLnsLensType tblLnsLensType)
		{

			objectSet.Add(tblLnsLensType);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblLnsLensType> tblLnsLensTypes)
		{
			objectSet.AddRange(tblLnsLensTypes);
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
