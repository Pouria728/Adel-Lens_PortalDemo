
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsLensTypeRRepository : IRepository<TblLnsLensTypeR>
    {
        ICollection<TblLnsLensTypeR> GetAll();
        TblLnsLensTypeR GetByKey(int _LensTypeRId);
		Task<(List<TblLnsLensTypeR>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsLensTypeR> tblLnsLensTypeRs);

		Task Update(TblLnsLensTypeR tblLnsLensTypeR);
		Task Update(List<TblLnsLensTypeR> tblLnsLensTypeRs);
		Task Add(TblLnsLensTypeR	 tblLnsLensTypeR);
		Task Add(List<TblLnsLensTypeR> tblLnsLensTypeRs);
	}
}
