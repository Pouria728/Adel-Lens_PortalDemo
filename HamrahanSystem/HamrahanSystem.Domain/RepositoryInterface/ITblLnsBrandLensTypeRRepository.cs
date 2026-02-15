
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsBrandLensTypeRRepository : IRepository<TblLnsBrandLensTypeR>
    {
        ICollection<TblLnsBrandLensTypeR> GetAll();
        TblLnsBrandLensTypeR GetByKey(int _BrandLensTypeRId);
		Task<(List<TblLnsBrandLensTypeR>, int)> GetAll(int BrandId,int? maxResult, int? Page, int? rowInPage,string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsBrandLensTypeR> tblLnsBrandLensTypeRs);

		Task Update(TblLnsBrandLensTypeR tblLnsBrandLensTypeR);
		Task Update(List<TblLnsBrandLensTypeR> tblLnsBrandLensTypeRs);
		Task Add(TblLnsBrandLensTypeR tblLnsBrandLensTypeR);
		Task Add(List<TblLnsBrandLensTypeR> tblLnsBrandLensTypeRs);
	}
}
