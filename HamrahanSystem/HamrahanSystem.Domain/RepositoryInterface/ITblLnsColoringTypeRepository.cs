
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsColoringTypeRepository : IRepository<TblLnsColoringType>
    {
        ICollection<TblLnsColoringType> GetAll();
        TblLnsColoringType GetByKey(int _ColoringTypeId);
		Task<(List<TblLnsColoringType>, int)> GetAll( int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsColoringType> tblLnsColoringTypes);

		Task Update(TblLnsColoringType tblLnsColoringType);
		Task Update(List<TblLnsColoringType> tblLnsColoringTypes);
		Task Add(TblLnsColoringType tblLnsColoringType);
		Task Add(List<TblLnsColoringType> tblLnsColoringTypes);
	}
}
