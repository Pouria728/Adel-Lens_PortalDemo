
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsFrameTypeRepository : EntityFrameworkRepository<TblLnsFrameType>, ITblLnsFrameTypeRepository
    {
        public TblLnsFrameTypeRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsFrameType> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblLnsFrameType GetByKey(int _FrameTypeId)
        {
            return objectSet.SingleOrDefault(e => e.FrameTypeId == _FrameTypeId);
        }
		public Task<(List<TblLnsFrameType>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Count();
			var item = objectSet.AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblLnsFrameType()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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
			objectSet.Where(e => e.FrameTypeId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblLnsFrameType> tblLnsFrameTypes)
		{
			objectSet.RemoveRange(tblLnsFrameTypes);

			return Task.CompletedTask;
		}

		public Task Update(TblLnsFrameType tblLnsFrameType)
		{
			var item = objectSet.Single(x => x.FrameTypeId == tblLnsFrameType.FrameTypeId);
			item.Code = tblLnsFrameType.Code;
			item.IsActive = tblLnsFrameType.IsActive;
			item.Name = tblLnsFrameType.Name;
			item.OrderId = tblLnsFrameType.OrderId;
			item.Description = tblLnsFrameType.Description;
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<TblLnsFrameType> tblLnsFrameTypes)
		{
			throw new NotImplementedException();
		}

		public Task Add(TblLnsFrameType tblLnsFrameType)
		{
			
			objectSet.Add(tblLnsFrameType);
			Save();
			return Task.CompletedTask;

		}

		public Task Add(List<TblLnsFrameType> tblLnsFrameTypes)
		{
			objectSet.AddRange(tblLnsFrameTypes);
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
