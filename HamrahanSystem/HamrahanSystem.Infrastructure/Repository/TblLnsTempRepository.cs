
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblLnsTempRepository : EntityFrameworkRepository<TblLnsTemp>, ITblLnsTempRepository
    {
        public TblLnsTempRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblLnsTemp> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblLnsTemp GetByKey(long _TempId)
        {
            return objectSet.SingleOrDefault(e => e.TempId == _TempId);
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
