
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface IBaseRequestStepRepository : IRepository<BaseRequestStep>
    {
        ICollection<BaseRequestStep> GetAll();
        BaseRequestStep GetByKey(int _BaseRequestStepId);
		Task<(List<BaseRequestStep>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<BaseRequestStep> tblLnsBrand);

		Task Update(BaseRequestStep tblLnsBrand);
		Task Update(List<BaseRequestStep> tblLnsBrand);
		Task Add(BaseRequestStep tblLnsBrand);
		Task Add(List<BaseRequestStep> tblLnsBrand);
	}
}
