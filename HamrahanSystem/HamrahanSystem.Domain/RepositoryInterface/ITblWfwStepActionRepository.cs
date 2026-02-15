
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblWfwStepActionRepository : IRepository<TblWfwStepAction>
    {
        ICollection<TblWfwStepAction> GetAll();
        TblWfwStepAction GetByKey(int _StepActionId);
    }
}
