
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblWfwCartableRepository : EntityFrameworkRepository<TblWfwCartable>, ITblWfwCartableRepository
    {
        public TblWfwCartableRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblWfwCartable> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblWfwCartable GetByKey(long _CartableId)
        {
            return objectSet.SingleOrDefault(e => e.CartableId == _CartableId);
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
