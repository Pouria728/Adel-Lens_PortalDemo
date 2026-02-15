
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsSphRepository : IRepository<TblLnsSph>
    {
        ICollection<TblLnsSph> GetAll();
        TblLnsSph GetByKey(int _SphId);
		Task<(List<TblLnsSph>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsSph> tblLnsSph);

		Task Update(TblLnsSph tblLnsSph);
		Task Update(List<TblLnsSph> tblLnsSph);
		Task Add(TblLnsSph tblLnsSph);
		Task Add(List<TblLnsSph> tblLnsSph);
	}
}
