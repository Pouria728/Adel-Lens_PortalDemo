
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class UserRoleRepository : EntityFrameworkRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<UserRole> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual UserRole GetByKey(long _UserRoleId)
        {
            return objectSet.SingleOrDefault(e => e.UserRoleId == _UserRoleId);
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
