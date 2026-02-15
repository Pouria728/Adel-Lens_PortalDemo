
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsOrderserviceRepository : EntityFrameworkRepository<TblLnsOrderservice>, ITblLnsOrderserviceRepository
    {
        public TblLnsOrderserviceRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsOrderservice> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblLnsOrderservice GetByKey(long _OrderServicesId)
        {
            return objectSet.SingleOrDefault(e => e.OrderServicesId == _OrderServicesId);
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
