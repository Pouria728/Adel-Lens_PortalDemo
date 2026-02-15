
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblWfwConditionRepository : IRepository<TblWfwCondition>
    {
        ICollection<TblWfwCondition> GetAll();
        TblWfwCondition GetByKey(int _ConditionId);
    }
}
