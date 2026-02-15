
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsBrandRepository : IRepository<TblLnsBrand>
    {
        ICollection<TblLnsBrand> GetAll();
		ICollection<TblLnsBrand> GetAllActive();
		TblLnsBrand GetByKey(int _BrandId);
		Task<(List<TblLnsBrand>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsBrand> tblLnsBrand);

		Task Update(TblLnsBrand tblLnsBrand);
		Task Update(List<TblLnsBrand> tblLnsBrand);
		Task Add(TblLnsBrand tblLnsBrand);
		Task Add(List<TblLnsBrand> tblLnsBrand);
	}
}
