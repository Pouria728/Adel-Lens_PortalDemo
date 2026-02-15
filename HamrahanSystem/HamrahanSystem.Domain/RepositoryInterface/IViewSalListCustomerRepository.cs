
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface IViewSalListCustomerRepository : IRepository<ViewSalListCustomer>
    {
        ICollection<ViewSalListCustomer> GetAll();
        ViewSalListCustomer GetByKey(long _ViewSalListCustomerId);
    }
}
