
using System;
using HamrahanSystem.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class EntityFrameworkUnitOfWorkFactory : IUnitOfWorkFactory
    {
        protected DbContext context = null;

        public EntityFrameworkUnitOfWorkFactory(DbContextOptions<AdelModel> options) : this(new AdelModel(options))
		{
        }

        public EntityFrameworkUnitOfWorkFactory(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this.context = context;
        }

        #region IUnitOfWorkFactory Members

        public virtual IUnitOfWork Create()
        {
            if (context == null)
                throw new InvalidOperationException("Context has not been initialized.");
            return new EntityFrameworkUnitOfWork(context);
        }
        #endregion
    }
}
