
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblWfwConditionRepository : EntityFrameworkRepository<TblWfwCondition>, ITblWfwConditionRepository
    {
        public TblWfwConditionRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblWfwCondition> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblWfwCondition GetByKey(int _ConditionId)
        {
            return objectSet.SingleOrDefault(e => e.ConditionId == _ConditionId);
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
