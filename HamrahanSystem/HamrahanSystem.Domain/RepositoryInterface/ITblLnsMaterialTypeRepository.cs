
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsMaterialTypeRepository : IRepository<TblLnsMaterialType>
    {
        ICollection<TblLnsMaterialType> GetAll();
        TblLnsMaterialType GetByKey(int _MaterialTypeId);
		Task<(List<TblLnsMaterialType>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsMaterialType> tblLnsBrand);

		Task Update(TblLnsMaterialType tblLnsBrand);
		Task Update(List<TblLnsMaterialType> tblLnsBrand);
		Task Add(TblLnsMaterialType tblLnsBrand);
		Task Add(List<TblLnsMaterialType> tblLnsBrand);
	}
}
