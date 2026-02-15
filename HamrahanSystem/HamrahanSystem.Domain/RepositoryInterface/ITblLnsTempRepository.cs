
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsTempRepository : IRepository<TblLnsTemp>
    {
        ICollection<TblLnsTemp> GetAll();
        TblLnsTemp GetByKey(long _TempId);
    }
}
