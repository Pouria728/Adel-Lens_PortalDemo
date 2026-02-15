
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblWfwStepActionRepository : EntityFrameworkRepository<TblWfwStepAction>, ITblWfwStepActionRepository
    {
        public TblWfwStepActionRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblWfwStepAction> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblWfwStepAction GetByKey(int _StepActionId)
        {
            return objectSet.SingleOrDefault(e => e.StepActionId == _StepActionId);
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
