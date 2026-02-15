
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsBrandCoatingRepository : EntityFrameworkRepository<TblLnsBrandCoating>, ITblLnsBrandCoatingRepository
    {
        public TblLnsBrandCoatingRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsBrandCoating> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblLnsBrandCoating GetByKey(int _BrandCoatingId)
        {
            return objectSet.SingleOrDefault(e => e.BrandCoatingId == _BrandCoatingId);
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
