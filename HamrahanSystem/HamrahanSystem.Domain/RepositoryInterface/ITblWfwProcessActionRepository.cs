
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblWfwProcessActionRepository : IRepository<TblWfwProcessAction>
    {
        ICollection<TblWfwProcessAction> GetAll();
        TblWfwProcessAction GetByKey(int _ProcessActionId);
    }
}
