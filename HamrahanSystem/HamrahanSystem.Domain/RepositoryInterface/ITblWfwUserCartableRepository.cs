
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblWfwUserCartableRepository : IRepository<TblWfwUserCartable>
    {
        ICollection<TblWfwUserCartable> GetAll();
        TblWfwUserCartable GetByKey(long _UserCartableId);
    }
}
