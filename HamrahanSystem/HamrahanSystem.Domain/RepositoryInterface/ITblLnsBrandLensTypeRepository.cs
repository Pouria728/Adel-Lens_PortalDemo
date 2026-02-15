
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsBrandLensTypeRepository : IRepository<TblLnsBrandLensType>
    {
        ICollection<TblLnsBrandLensType> GetAll();
        TblLnsBrandLensType GetByKey(int _BrandLensTypeId);
		Task<(List<TblLnsBrandLensType>, int)> GetAll(int BrandId,int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsBrandLensType> tblLnsBrandLensTypes);

		Task Update(TblLnsBrandLensType tblLnsBrandLensType);
		Task Update(List<TblLnsBrandLensType> tblLnsBrandLensTypes);
		Task Add(TblLnsBrandLensType tblLnsBrandLensType);
		Task Add(List<TblLnsBrandLensType> tblLnsBrandLensTypes);
	}
}
