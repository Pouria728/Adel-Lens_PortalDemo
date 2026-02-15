
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblWfwOrderProcessStepRepository : IRepository<TblWfwOrderProcessStep>
    {
        ICollection<TblWfwOrderProcessStep> GetAll();
        TblWfwOrderProcessStep GetByKey(long _OrderId);
		Task<(List<TblWfwOrderProcessStep>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<(List<TblWfwOrderProcessStep>, int)> GetAllByFilter(int? requestStatusId, int? orderStatusId, int? indexDocument, int? customerId, int? roleId, int? createdById, int? maxResult, int? page, int? rowInPage, string sort, string sidx, string factorNo = "", string? fromDate = null, string? toDate = null);
		Task Delete(int _ID);
		Task Delete(List<TblWfwOrderProcessStep> tblLnsBrand);

		Task Update(TblWfwOrderProcessStep tblLnsBrand);
		Task Update(List<TblWfwOrderProcessStep> tblLnsBrand);
		Task Add(TblWfwOrderProcessStep tblLnsBrand);
		Task Add(List<TblWfwOrderProcessStep> tblLnsBrand);
	}
}
