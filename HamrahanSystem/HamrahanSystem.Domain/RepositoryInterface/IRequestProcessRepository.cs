
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface IRequestProcessRepository : IRepository<RequestProcess>
    {
        ICollection<RequestProcess> GetAll();
        RequestProcess GetByKey(Guid _RequestProcessId);
		Task<(List<RequestProcess>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(Guid _ID);
		Task Delete(List<RequestProcess> tblLnsBrand);

		Task Update(RequestProcess tblLnsBrand);
		Task Update(List<RequestProcess> tblLnsBrand);
		Task Add(RequestProcess tblLnsBrand);
		Task Add(List<RequestProcess> tblLnsBrand);
	}
}
