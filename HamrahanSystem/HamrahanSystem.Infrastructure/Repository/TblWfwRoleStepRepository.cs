
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblWfwRoleStepRepository : EntityFrameworkRepository<TblWfwRoleStep>, ITblWfwRoleStepRepository
    {
        public TblWfwRoleStepRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblWfwRoleStep> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblWfwRoleStep GetByKey(int _RoleStepId)
        {
            return objectSet.SingleOrDefault(e => e.RoleStepId == _RoleStepId);
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
