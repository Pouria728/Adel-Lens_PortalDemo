
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsLensIndexRepository : IRepository<TblLnsLensIndex>
    {
        ICollection<TblLnsLensIndex> GetAll();
        TblLnsLensIndex GetByKey(int _LensIndexId);
		Task<(List<TblLnsLensIndex>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsLensIndex> tblLnsLensIndex);

		Task Update(TblLnsLensIndex tblLnsLensIndex);
		Task Update(List<TblLnsLensIndex> tblLnsLensIndex);
		Task Add(TblLnsLensIndex tblLnsLensIndex);
		Task Add(List<TblLnsLensIndex> tblLnsLensIndex);
	}
}
