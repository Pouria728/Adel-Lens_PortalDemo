
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblWfwProcessStepRepository : IRepository<TblWfwProcessStep>
    {
        ICollection<TblWfwProcessStep> GetAll();
        TblWfwProcessStep GetByKey(int _ProcessStepId);
		Task<(List<TblWfwProcessStep>, int)> GetAll(int processId,int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblWfwProcessStep> tblWfwProcessSteps);

		Task Update(TblWfwProcessStep tblWfwProcessStep);
		Task Update(List<TblWfwProcessStep> tblWfwProcessSteps);
		Task<int> Add(TblWfwProcessStep tblWfwProcessStep);
		Task Add(List<TblWfwProcessStep> tblWfwProcessSteps);
	}
}
