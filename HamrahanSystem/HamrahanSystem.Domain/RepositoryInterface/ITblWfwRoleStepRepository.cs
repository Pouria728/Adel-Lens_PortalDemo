
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;


namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblWfwRoleStepRepository : IRepository<TblWfwRoleStep>
    {
        ICollection<TblWfwRoleStep> GetAll();
        TblWfwRoleStep GetByKey(int _RoleStepId);
    }
}
