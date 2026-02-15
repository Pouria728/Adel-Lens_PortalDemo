
using System;
using System.Linq;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;


namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class RolePermissionRepository : EntityFrameworkRepository<RolePermission>, IRolePermissionRepository
    {
        public RolePermissionRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<RolePermission> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual RolePermission GetByKey(long _RolePermissionId)
        {
            return objectSet.SingleOrDefault(e => e.RolePermissionId == _RolePermissionId);
        }

        public new AdelModel Context 
        {
            get
            {
                return (AdelModel)base.Context;
            }
        }
    }
}
