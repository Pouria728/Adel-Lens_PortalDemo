
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class UserPermissionRepository : EntityFrameworkRepository<UserPermission>, IUserPermissionRepository
    {
        public UserPermissionRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<UserPermission> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual UserPermission GetByKey(long _UserPermissionId)
        {
            return objectSet.SingleOrDefault(e => e.UserPermissionId == _UserPermissionId);
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
