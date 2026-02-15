
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblWfwRelationStepConditionRepository : EntityFrameworkRepository<TblWfwRelationStepCondition>, ITblWfwRelationStepConditionRepository
    {
        public TblWfwRelationStepConditionRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblWfwRelationStepCondition> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblWfwRelationStepCondition GetByKey(int _RelationStepConditionId)
        {
            return objectSet.SingleOrDefault(e => e.RelationStepConditionId == _RelationStepConditionId);
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
