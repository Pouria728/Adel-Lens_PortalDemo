
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsMaterialTypeRepository : EntityFrameworkRepository<TblLnsMaterialType>, ITblLnsMaterialTypeRepository
    {
        public TblLnsMaterialTypeRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsMaterialType> GetAll()
        {
            return objectSet.ToList();
        }
		public virtual Task<(List<TblLnsMaterialType>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Count();
			var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsMaterialType()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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

		public virtual TblLnsMaterialType GetByKey(int _MaterialTypeId)
        {
            return objectSet.SingleOrDefault(e => e.MaterialTypeId == _MaterialTypeId);
        }
		public Task Update(TblLnsMaterialType tblLnsMaterialType)
		{
			var item = objectSet.Single(x => x.MaterialTypeId == tblLnsMaterialType.MaterialTypeId);
			item.Name = tblLnsMaterialType.Name;
			item.IsActive = tblLnsMaterialType.IsActive;
			item.Code = tblLnsMaterialType.Code;
			item.Description = tblLnsMaterialType.Description;
			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.MaterialTypeId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsMaterialType> TblLnsBrands)
		{
			objectSet.RemoveRange(TblLnsBrands);
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsMaterialType> TblLnsBrands)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblLnsMaterialType tblLnsMaterialType)
		{

			objectSet.Add(tblLnsMaterialType);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblLnsMaterialType> tblLnsMaterialTypes)
		{
			objectSet.AddRange(tblLnsMaterialTypes);
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
