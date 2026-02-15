
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblWfwCartableRepository : IRepository<TblWfwCartable>
    {
        ICollection<TblWfwCartable> GetAll();
        TblWfwCartable GetByKey(long _CartableId);
    }
}
