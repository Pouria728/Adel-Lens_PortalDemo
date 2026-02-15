
using System;
using System.Linq;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class TblClrDefineObjectRepository : EntityFrameworkRepository<TblClrDefineObject>, ITblClrDefineObjectRepository
    {
        public TblClrDefineObjectRepository(AdelModel context)
            : base(context)
        {
        }

        public virtual ICollection<TblClrDefineObject> GetAll()
        {
            return objectSet.ToList();
        }
		public virtual ICollection<TblClrDefineObject> GetByName(string name)
		{
			return objectSet.Where(x=>x.NameObject.Contains(name)&& x.IsActive==1).ToList();
		}
		public virtual ICollection<TblClrDefineObject> Search(string term)
		{
			if (string.IsNullOrWhiteSpace(term))
				return new List<TblClrDefineObject>();

			term = term.Trim();
			return objectSet
				.Where(x =>
					(x.NameObject != null && x.NameObject.Contains(term)) ||
					(x.LatinNameObject != null && x.LatinNameObject.Contains(term)) ||
					(x.CodeObject != null && x.CodeObject.Contains(term)) ||
					(x.TechnicalSpecs != null && x.TechnicalSpecs.Contains(term)))
				.Where(x => x.IsActive == 1 || x.IsActive == null)
				.ToList();
		}
		public virtual TblClrDefineObject GetById(int defineObjectId)
		{
			return objectSet.SingleOrDefault(e => e.DefineObjectId==defineObjectId);
		}

		public virtual TblClrDefineObject GetByKey(byte _Company, int _RecNo)
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
