
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface IUserRepository : IRepository<User>
    {
        ICollection<User> GetAll();
		Task<(List<User>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		User GetByKey(int _UserId);
		List<User>  GetByUserName(string username);
		Task Delete(int _ID);
		Task Delete(List<User> tblLnsBrand);

		Task Update(User tblLnsBrand);
		Task Update(List<User> tblLnsBrand);
		Task Add(User tblLnsBrand);
		Task Add(List<User> tblLnsBrand);
	}
}
