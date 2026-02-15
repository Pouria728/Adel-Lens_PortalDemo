
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblWfwOrderProcessRepository : IRepository<TblWfwOrderProcess>
    {
        ICollection<TblWfwOrderProcess> GetAll();
        TblWfwOrderProcess GetByKey(long _OrderId);
		Task<(List<TblWfwOrderProcess>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<(List<TblWfwOrderProcess>, int)> GetAllByFilter(int? OrderStatusId, int? IndexDocument, int? CustomerId, int? UserId, int? maxResult, int? page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblWfwOrderProcess> tblLnsBrand);

		Task Update(TblWfwOrderProcess tblLnsBrand);
		Task Update(List<TblWfwOrderProcess> tblLnsBrand);
		Task Add(TblWfwOrderProcess tblLnsBrand);
		Task Add(List<TblWfwOrderProcess> tblLnsBrand);
	}
}
