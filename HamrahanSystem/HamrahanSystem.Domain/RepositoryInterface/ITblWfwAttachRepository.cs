
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblWfwAttachRepository : IRepository<TblWfwAttach>
    {
        ICollection<TblWfwAttach> GetAll();
        TblWfwAttach GetByKey(long _AttachId);
    }
}
