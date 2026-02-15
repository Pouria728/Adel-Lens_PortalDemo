
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsOrderserviceRepository : IRepository<TblLnsOrderservice>
    {
        ICollection<TblLnsOrderservice> GetAll();
        TblLnsOrderservice GetByKey(long _OrderServicesId);
    }
}
