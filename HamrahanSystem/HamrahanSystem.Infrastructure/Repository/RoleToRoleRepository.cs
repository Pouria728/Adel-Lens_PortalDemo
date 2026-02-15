
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class RoleToRoleRepository : EntityFrameworkRepository<RoleToRole>, IRoleToRoleRepository
    {
        public RoleToRoleRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<RoleToRole> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual RoleToRole GetByKey(long _RoleToRolesId)
        {
            return objectSet.SingleOrDefault(e => e.RoleToRolesId == _RoleToRolesId);
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
