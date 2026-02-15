
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblWfwStatusRepository : IRepository<TblWfwStatus>
    {
        ICollection<TblWfwStatus> GetAll();
        TblWfwStatus GetByKey(int _StatusId);
    }
}
