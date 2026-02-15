
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblWfwResultStepRepository : IRepository<TblWfwResultStep>
    {
        ICollection<TblWfwResultStep> GetAll();
        TblWfwResultStep GetByKey(int _ResultStepId);
    }
}
