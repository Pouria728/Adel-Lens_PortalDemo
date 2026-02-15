
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsLensTypeRepository : IRepository<TblLnsLensType>
    {
        ICollection<TblLnsLensType> GetAll();
        TblLnsLensType GetByKey(int _LensTypeId);
		Task<(List<TblLnsLensType>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsLensType> tblLnsLensTypes);

		Task Update(TblLnsLensType tblLnsLensType);
		Task Update(List<TblLnsLensType> tblLnsLensTypes);
		Task Add(TblLnsLensType tblLnsLensType);
		Task Add(List<TblLnsLensType> tblLnsLensTypes);
	}
}
