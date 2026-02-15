
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsLensIndexRRepository : IRepository<TblLnsLensIndexR>
    {
        ICollection<TblLnsLensIndexR> GetAll();
        TblLnsLensIndexR GetByKey(int _LensIndexRId);
		Task<(List<TblLnsLensIndexR>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsLensIndexR> tblLnsLensIndexR);

		Task Update(TblLnsLensIndexR tblLnsLensIndexR);
		Task Update(List<TblLnsLensIndexR> tblLnsLensIndexR);
		Task Add(TblLnsLensIndexR tblLnsLensIndexR);
		Task Add(List<TblLnsLensIndexR> tblLnsLensIndexR);
	}
}
