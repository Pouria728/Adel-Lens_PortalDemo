
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsColoringTypeRepository : EntityFrameworkRepository<TblLnsColoringType>, ITblLnsColoringTypeRepository
    {
        public TblLnsColoringTypeRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsColoringType> GetAll()
        {
            return objectSet.ToList();
        }
		public Task<(List<TblLnsColoringType>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Count();
			var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsColoringType()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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

		public virtual TblLnsColoringType GetByKey(int _ColoringTypeId)
        {
            return objectSet.SingleOrDefault(e => e.ColoringTypeId == _ColoringTypeId);
        }
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.ColoringTypeId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsColoringType> tblLnsColoringTypes)
		{
			objectSet.RemoveRange(tblLnsColoringTypes);

			return Task.CompletedTask;
		}

		public Task Update(TblLnsColoringType tblLnsColoringType)
		{
			var item = objectSet.Single(x => x.ColoringTypeId == tblLnsColoringType.ColoringTypeId);
			item.Code = tblLnsColoringType.Code;
			item.IsActive = tblLnsColoringType.IsActive;
			item.Name = tblLnsColoringType.Name;
			item.OrderId = tblLnsColoringType.OrderId;
			item.Description = tblLnsColoringType.Description;
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsColoringType> tblLnsColoringTypes)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblLnsColoringType tblLnsColoringType)
		{

			objectSet.Add(tblLnsColoringType);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblLnsColoringType> tblLnsColoringTypes)
		{
			objectSet.AddRange(tblLnsColoringTypes);
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
