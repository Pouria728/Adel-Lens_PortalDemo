
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface IRequestProcessStepRepository : IRepository<RequestProcessStep>
    {
        ICollection<RequestProcessStep> GetAll();
        RequestProcessStep GetByKey(Guid _RequestProcessStepId);
		Task<(List<RequestProcessStep>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(Guid _ID);
		Task Delete(List<RequestProcessStep> tblLnsBrand);

		Task Update(RequestProcessStep tblLnsBrand);
		Task Update(List<RequestProcessStep> tblLnsBrand);
		Task Add(RequestProcessStep tblLnsBrand);
		Task Add(List<RequestProcessStep> tblLnsBrand);
	}
}
