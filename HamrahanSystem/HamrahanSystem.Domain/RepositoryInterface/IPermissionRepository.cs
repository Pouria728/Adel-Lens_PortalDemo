
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface IPermissionRepository : IRepository<Permission>
    {
        ICollection<Permission> GetAll();
		Permission GetByKey(int _PermissionId);

	}
}
