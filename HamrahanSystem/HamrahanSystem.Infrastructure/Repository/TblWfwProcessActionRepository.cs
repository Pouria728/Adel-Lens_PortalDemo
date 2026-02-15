
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblWfwProcessActionRepository : EntityFrameworkRepository<TblWfwProcessAction>, ITblWfwProcessActionRepository
    {
        public TblWfwProcessActionRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblWfwProcessAction> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblWfwProcessAction GetByKey(int _ProcessActionId)
        {
            return objectSet.SingleOrDefault(e => e.ProcessActionId == _ProcessActionId);
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
