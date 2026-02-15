
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblWfwProcessStepRepository : EntityFrameworkRepository<TblWfwProcessStep>, ITblWfwProcessStepRepository
    {
        public TblWfwProcessStepRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblWfwProcessStep> GetAll()
        {
            return objectSet.ToList();
        }

     
		public virtual Task<(List<TblWfwProcessStep>, int)> GetAll(int processId, int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var countList = objectSet.Where(x => x.ProcessId == processId).Count();
			var item = objectSet.Where(x=>x.ProcessId==processId).AsQueryable();
            if (!string.IsNullOrEmpty(sidx))
            {
                var colsort = (new TblWfwProcessStep()).GetType().GetMembers().SingleOrDefault(y => y.Name.ToLower() == sidx.ToLower())?.Name;
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

		public virtual TblWfwProcessStep GetByKey(int _ProcessStepId)
		{
			return objectSet.Include(x=>x.TblWfwProcess).Include(x=>x.TblWfwRoleStepes).SingleOrDefault(e => e.ProcessStepId == _ProcessStepId);
		}
		public Task Update(TblWfwProcessStep tblWfwProcessStep)
		{
			var item = objectSet.Include(x=>x.TblWfwRoleStepes).Single(x => x.ProcessStepId == tblWfwProcessStep.ProcessStepId);
			item.TblWfwRoleStepes.Clear();
			item.TblWfwRoleStepes=tblWfwProcessStep.TblWfwRoleStepes;
			item.Name = tblWfwProcessStep.Name;
			item.IsActive = tblWfwProcessStep.IsActive;
			item.Code = tblWfwProcessStep.Code;
			item.Description = tblWfwProcessStep.Description;
			item.IsPrint = tblWfwProcessStep.IsPrint;
			item.IsBarcode = tblWfwProcessStep.IsBarcode;
			item.ProcedureId = tblWfwProcessStep.ProcedureId;
			item.OrderId = tblWfwProcessStep.OrderId;


			Save();
			return Task.CompletedTask;
		}
		public Task Delete(int _ID)
		{
			objectSet.Where(e => e.ProcessStepId == _ID).ExecuteDelete();
			return Task.CompletedTask;
		}

		public Task Delete(List<TblWfwProcessStep> tblWfwProcessSteps)
		{
			objectSet.RemoveRange(tblWfwProcessSteps);
			Save();
			return Task.CompletedTask;
		}


		public Task Update(List<TblWfwProcessStep> tblWfwProcessSteps)
		{
			throw new NotImplementedException();
		}

		public Task<int> Add(TblWfwProcessStep tblWfwProcessStep)
		{

			objectSet.Add(tblWfwProcessStep);
			Save();
			return Task.FromResult(tblWfwProcessStep.ProcessStepId);

		}

		public Task Add(List<TblWfwProcessStep> tblWfwProcessSteps)
		{
			objectSet.AddRange(tblWfwProcessSteps);
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
