
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsLensIndexRSphRepository : IRepository<TblLnsLensIndexRSph>
    {
		
		ICollection<TblLnsLensIndexRSph> GetAll();
        TblLnsLensIndexRSph GetByKey(int _LensIndexRSphId);
		Task<(List<TblLnsLensIndexRSph>, int)> GetAll(int lensTypeRLensIndexRId,int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsLensIndexRSph> tblLnsLensIndexRSphs);

		Task Update(TblLnsLensIndexRSph tblLnsLensIndexRSph);
		Task Update(List<TblLnsLensIndexRSph> tblLnsLensIndexRSphs);
		Task Add(TblLnsLensIndexRSph tblLnsLensIndexRSph);
		Task Add(List<TblLnsLensIndexRSph> tblLnsLensIndexRSphs);
	}
}
