
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblAccDefineCostCenterRepository : IRepository<TblAccDefineCostCenter>
    {
        ICollection<TblAccDefineCostCenter> GetAll();
        ICollection<TblAccDefineCostCenter> GetByName(string name);

        TblAccDefineCostCenter GetByKey(byte _Company, int _RecNo);
        TblAccDefineCostCenter GetById(int defineObjectId);

	}
}
