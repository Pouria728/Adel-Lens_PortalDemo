
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblDefineServiceRepository : EntityFrameworkRepository<TblDefineService>, ITblDefineServiceRepository
    {
        public TblDefineServiceRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblDefineService> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblDefineService GetByKey(byte _Company, int _RecNo)
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
