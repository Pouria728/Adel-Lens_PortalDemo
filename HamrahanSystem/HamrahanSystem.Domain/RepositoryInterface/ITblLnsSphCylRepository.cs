
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsSphCylRepository : IRepository<TblLnsSphCyl>
    {
        ICollection<TblLnsSphCyl> GetAll();
        TblLnsSphCyl GetByKey(int _SphCylId);
		Task<(List<TblLnsSphCyl>, int)> GetAll(int lensIndexRSphId, int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsSphCyl> tblLnsBrand);

		Task Update(TblLnsSphCyl tblLnsBrand);
		Task Update(List<TblLnsSphCyl> tblLnsBrand);
		Task Add(TblLnsSphCyl tblLnsBrand);
		Task Add(List<TblLnsSphCyl> tblLnsBrand);
	}
}
