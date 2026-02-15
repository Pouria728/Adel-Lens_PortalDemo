
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblWfwAttachRepository : EntityFrameworkRepository<TblWfwAttach>, ITblWfwAttachRepository
    {
        public TblWfwAttachRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblWfwAttach> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblWfwAttach GetByKey(long _AttachId)
        {
            return objectSet.SingleOrDefault(e => e.AttachId == _AttachId);
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
