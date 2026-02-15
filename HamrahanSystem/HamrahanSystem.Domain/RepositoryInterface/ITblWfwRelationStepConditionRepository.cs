
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblWfwRelationStepConditionRepository : IRepository<TblWfwRelationStepCondition>
    {
        ICollection<TblWfwRelationStepCondition> GetAll();
        TblWfwRelationStepCondition GetByKey(int _RelationStepConditionId);
    }
}
