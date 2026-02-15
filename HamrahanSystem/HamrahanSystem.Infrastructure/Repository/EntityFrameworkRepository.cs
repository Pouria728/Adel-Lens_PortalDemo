
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace HamrahanSystem.Infrastructure.Repository
{
    public partial class EntityFrameworkRepository<T> : IRepository<T> where T : class
    {
        private DbContext context;
        protected DbSet<T> objectSet;

        public EntityFrameworkRepository(DbContext context)
        {

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.context = context;
            this.objectSet = context.Set<T>();
        }

        public virtual void Add(T entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            objectSet.Add(entity);
        }

        public virtual void Remove(T entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            objectSet.Remove(entity);
        }
		public virtual void Save()
		{
			Context.SaveChanges();
		}

		public DbContext Context 
        {
            get
            {
                return context;
            }
        }
    }
}
