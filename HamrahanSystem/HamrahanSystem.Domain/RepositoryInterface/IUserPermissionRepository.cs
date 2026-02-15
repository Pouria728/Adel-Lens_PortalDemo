
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface IUserPermissionRepository : IRepository<UserPermission>
    {
        ICollection<UserPermission> GetAll();
        UserPermission GetByKey(long _UserPermissionId);
    }
}
