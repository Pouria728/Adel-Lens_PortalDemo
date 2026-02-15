
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface IRequestFieldeRepository : IRepository<RequestFielde>
    {
        ICollection<RequestFielde> GetAll();
        RequestFielde GetByKey(Guid _RequestFieldeId);
		Task<(List<RequestFielde>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(Guid _ID);
		Task Delete(List<RequestFielde> tblLnsBrand);

		Task Update(RequestFielde tblLnsBrand);
		Task Update(List<RequestFielde> tblLnsBrand);
		Task Add(RequestFielde tblLnsBrand);
		Task Add(List<RequestFielde> tblLnsBrand);
	}
}
