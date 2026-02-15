
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblAccDefineCostCenterRepository : EntityFrameworkRepository<TblAccDefineCostCenter>, ITblAccDefineCostCenterRepository
    {
        public TblAccDefineCostCenterRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblAccDefineCostCenter> GetAll()
        {
            return objectSet.ToList();
        }
		public virtual ICollection<TblAccDefineCostCenter> GetByName(string name)
		{
			return objectSet.Where(x=>x.NameCostCenter.Contains(name)&& x.IsActive==1).ToList();
		}
		public virtual TblAccDefineCostCenter GetById(int defineObjectId)
		{
			return objectSet.SingleOrDefault(e => e.AccDefineCostCenterId==defineObjectId);
		}

		public virtual TblAccDefineCostCenter GetByKey(byte _Company, int _RecNo)
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
