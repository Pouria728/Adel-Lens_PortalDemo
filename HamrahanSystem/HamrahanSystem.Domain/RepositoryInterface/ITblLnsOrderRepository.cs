
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsOrderRepository : IRepository<TblLnsOrder>
    {
        ICollection<TblLnsOrder> GetAll();
        TblLnsOrder GetByKey(long _OrderId);
		Task<(List<TblLnsOrder>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<(List<TblLnsOrder>, int)> GetAllByFilter(int? OrderStatusId, int? IndexDocument, int? CustomerId, int? UserId, int? maxResult, int? page, int? rowInPage, string sort, string sidx, string search = "", string? fromDate = null, string? toDate = null);
		Task Delete(int _ID);
		Task Delete(List<TblLnsOrder> tblLnsBrand);

		Task Update(TblLnsOrder tblLnsBrand);
		Task Update(List<TblLnsOrder> tblLnsBrand);
		Task<TblLnsOrder> Add(TblLnsOrder tblLnsBrand);
		Task Add(List<TblLnsOrder> tblLnsBrand);
	}
}
