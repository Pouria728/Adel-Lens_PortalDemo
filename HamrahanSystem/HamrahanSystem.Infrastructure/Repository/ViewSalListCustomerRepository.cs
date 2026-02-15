
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class ViewSalListCustomerRepository : EntityFrameworkRepository<ViewSalListCustomer>, IViewSalListCustomerRepository
    {
        public ViewSalListCustomerRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<ViewSalListCustomer> GetAll()
        {
            return objectSet.Distinct().ToList();
        }

        public virtual ViewSalListCustomer GetByKey(long _ViewSalListCustomerId)
        {
            return objectSet.SingleOrDefault(e => e.DefineCustomerId == _ViewSalListCustomerId);
        }

        public new AdelModel Context 
        {
            get
            {
                return (AdelModel)base.Context;
            }
        }
    }
}
