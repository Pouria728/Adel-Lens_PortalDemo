
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblInfoCustomerRepository : EntityFrameworkRepository<TblInfoCustomer>, ITblInfoCustomerRepository
    {
        public TblInfoCustomerRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblInfoCustomer> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblInfoCustomer GetByKey(byte _Company, int _RecNo)
        {
            return objectSet.SingleOrDefault(e => e.Company == _Company && e.RecNo == _RecNo);
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
