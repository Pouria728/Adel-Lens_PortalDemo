
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblWfwResultStepRepository : EntityFrameworkRepository<TblWfwResultStep>, ITblWfwResultStepRepository
    {
        public TblWfwResultStepRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblWfwResultStep> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual TblWfwResultStep GetByKey(int _ResultStepId)
        {
            return objectSet.SingleOrDefault(e => e.ResultStepId == _ResultStepId);
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
