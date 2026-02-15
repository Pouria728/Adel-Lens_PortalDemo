
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface IBaseRequestFieldRepository : IRepository<BaseRequestField>
    {
        ICollection<BaseRequestField> GetAll();
        BaseRequestField GetByKey(int _BaseRequestFieldedId);
		Task<(List<BaseRequestField>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<BaseRequestField> tblLnsBrand);

		Task Update(BaseRequestField tblLnsBrand);
		Task Update(List<BaseRequestField> tblLnsBrand);
		Task Add(BaseRequestField tblLnsBrand);
		Task Add(List<BaseRequestField> tblLnsBrand);
	}
}
