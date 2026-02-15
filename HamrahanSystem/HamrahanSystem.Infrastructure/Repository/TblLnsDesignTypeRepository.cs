
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsDesignTypeRepository : EntityFrameworkRepository<TblLnsDesignType>, ITblLnsDesignTypeRepository
    {
        public TblLnsDesignTypeRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsDesignType> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblLnsDesignType GetByKey(int _DesignTypeId)
        {
            return objectSet.SingleOrDefault(e => e.DesignTypeId == _DesignTypeId);
        }
		public Task<(List<TblLnsDesignType>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Count();
			var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsDesignType()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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
			objectSet.Where(e => e.DesignTypeId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsDesignType> tblLnsDesignTypes)
		{
			objectSet.RemoveRange(tblLnsDesignTypes);

			return Task.CompletedTask;
		}

		public Task Update(TblLnsDesignType tblLnsDesignType)
		{
			var item = objectSet.Single(x => x.DesignTypeId == tblLnsDesignType.DesignTypeId);
			item.Code = tblLnsDesignType.Code;
            item.IsActive = tblLnsDesignType.IsActive;
            item.Description = tblLnsDesignType.Description;
            item.IsSpecial = tblLnsDesignType.IsSpecial;
            item.SphPlus = tblLnsDesignType.SphPlus;
            item.SphMinus = tblLnsDesignType.SphMinus;
            item.Addition = tblLnsDesignType.Addition;
            item.AdditionValue = tblLnsDesignType.AdditionValue;
            item.LensTypeId = tblLnsDesignType.LensTypeId;
			item.Name = tblLnsDesignType.Name;
			item.OrderId = tblLnsDesignType.OrderId;
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsDesignType> tblLnsDesignTypes)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblLnsDesignType tblLnsDesignType)
		{

			objectSet.Add(tblLnsDesignType);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblLnsDesignType> tblLnsDesignTypes)
		{
			objectSet.AddRange(tblLnsDesignTypes);
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
