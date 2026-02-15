
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblInfoCustomerRepository : IRepository<TblInfoCustomer>
    {
        ICollection<TblInfoCustomer> GetAll();
        TblInfoCustomer GetByKey(byte _Company, int _RecNo);
    }
}
