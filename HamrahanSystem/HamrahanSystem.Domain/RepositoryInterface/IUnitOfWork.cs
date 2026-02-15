
using System;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
