
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsLensTypeRLensIndexRRepository : IRepository<TblLnsLensTypeRLensIndexR>
    {
        ICollection<TblLnsLensTypeRLensIndexR> GetAll();
        TblLnsLensTypeRLensIndexR GetByKey(int _LensTypeRLensIndexRId);
		Task<(List<TblLnsLensTypeRLensIndexR>, int)> GetAll(int BrandLensTypeRId,int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsLensTypeRLensIndexR> tblLnsLensTypeRLensIndexRs);

		Task Update(TblLnsLensTypeRLensIndexR tblLnsLensTypeRLensIndexR);
		Task Update(List<TblLnsLensTypeRLensIndexR> tblLnsLensTypeRLensIndexRs);
		Task Add(TblLnsLensTypeRLensIndexR tblLnsLensTypeRLensIndexR);
		Task Add(List<TblLnsLensTypeRLensIndexR> tblLnsLensTypeRLensIndexRs);
	}
}
