
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblWfwUserCartableRepository : EntityFrameworkRepository<TblWfwUserCartable>, ITblWfwUserCartableRepository
    {
        public TblWfwUserCartableRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblWfwUserCartable> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblWfwUserCartable GetByKey(long _UserCartableId)
        {
            return objectSet.SingleOrDefault(e => e.UserCartableId == _UserCartableId);
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
