
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface IRoleRepository : IRepository<Role>
    {
        ICollection<Role> GetAll();
        Role GetByKey(int _RoleId);
		Task<(List<Role>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<Role> tblLnsBrand);

		Task Update(Role tblLnsBrand);
		Task Update(List<Role> tblLnsBrand);
		Task Add(Role tblLnsBrand);
		Task Add(List<Role> tblLnsBrand);
	}
}
