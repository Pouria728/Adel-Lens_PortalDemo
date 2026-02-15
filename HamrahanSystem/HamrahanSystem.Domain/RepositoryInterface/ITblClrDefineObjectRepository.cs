
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblClrDefineObjectRepository : IRepository<TblClrDefineObject>
    {
        ICollection<TblClrDefineObject> GetAll();
        ICollection<TblClrDefineObject> GetByName(string name);
        ICollection<TblClrDefineObject> Search(string term);

		TblClrDefineObject GetByKey(byte _Company, int _RecNo);
        TblClrDefineObject GetById(int defineObjectId);

	}
}
