
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblWfwStatusRepository : EntityFrameworkRepository<TblWfwStatus>, ITblWfwStatusRepository
    {
        public TblWfwStatusRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblWfwStatus> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblWfwStatus GetByKey(int _StatusId)
        {
            return objectSet.SingleOrDefault(e => e.StatusId == _StatusId);
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
