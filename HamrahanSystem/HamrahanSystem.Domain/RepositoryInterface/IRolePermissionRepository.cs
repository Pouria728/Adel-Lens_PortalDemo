
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;


namespace HamrahanSystem.Domain.Repository
{
    public partial interface IRolePermissionRepository : IRepository<RolePermission>
    {
        ICollection<RolePermission> GetAll();
        RolePermission GetByKey(long _RolePermissionId);
    }
}
