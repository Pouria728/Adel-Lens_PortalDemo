
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsCylRepository : IRepository<TblLnsCyl>
    {
        ICollection<TblLnsCyl> GetAll();
        TblLnsCyl GetByKey(int _CylId);
		Task<(List<TblLnsCyl>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsCyl> tblLnsCyl);

		Task Update(TblLnsCyl tblLnsCyl);
		Task Update(List<TblLnsCyl> tblLnsCyl);
		Task Add(TblLnsCyl tblLnsCyl);
		Task Add(List<TblLnsCyl> tblLnsCyl);
	}
}
