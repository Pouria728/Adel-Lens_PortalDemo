
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface IRequestRepository : IRepository<Request>
    {
        ICollection<Request> GetAll();
        Request GetByKey(Guid _RequestId);
		Task<(List<Request>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(Guid _ID);
		Task Delete(List<Request> tblLnsBrand);

		Task Update(Request tblLnsBrand);
		Task Update(List<Request> tblLnsBrand);
		Task Add(Request tblLnsBrand);
		Task Add(List<Request> tblLnsBrand);
	}
}
