
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface IBaseRequesteRepository : IRepository<BaseRequeste>
    {
        IList<BaseRequeste> GetAll();
        BaseRequeste GetByKey(int _BaseRequesteId);
		Task<(List<BaseRequeste>, int)> GetAll(int? maxResult, int? Page, int? rowInPag,string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<BaseRequeste> tblLnsBrand);

		Task Update(BaseRequeste tblLnsBrand);
		Task Update(List<BaseRequeste> tblLnsBrand);
		Task Add(BaseRequeste tblLnsBrand);
		Task Add(List<BaseRequeste> tblLnsBrand);
	}
}
