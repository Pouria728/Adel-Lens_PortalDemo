
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblDefineServiceRepository : IRepository<TblDefineService>
    {
        ICollection<TblDefineService> GetAll();
        TblDefineService GetByKey(byte _Company, int _RecNo);
    }
}
