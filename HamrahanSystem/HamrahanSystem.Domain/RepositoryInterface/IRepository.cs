
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface IRepository<T>
    {
        void Add(T entity);
        void Remove(T entity);
    }
}
