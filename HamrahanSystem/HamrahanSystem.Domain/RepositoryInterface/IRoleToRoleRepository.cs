
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface IRoleToRoleRepository : IRepository<RoleToRole>
    {
        ICollection<RoleToRole> GetAll();
        RoleToRole GetByKey(long _RoleToRolesId);
    }
}
