
using System;
using System.Linq;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;


namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class PermissionRepository : EntityFrameworkRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<Permission> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual Permission GetByKey(int _PermissionId)
        {
            return objectSet.SingleOrDefault(e => e.PermissionId == _PermissionId);
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
