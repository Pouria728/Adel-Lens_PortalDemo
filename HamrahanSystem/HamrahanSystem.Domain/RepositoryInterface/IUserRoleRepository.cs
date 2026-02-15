
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface IUserRoleRepository : IRepository<UserRole>
    {
        ICollection<UserRole> GetAll();
        UserRole GetByKey(long _UserRoleId);
    }
}
