using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onLineShop.Contracts.Repositories;
using onLineShop.DAL.Data;

namespace onLineShop.DAL.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        internal DataContext Context;
        internal DbSet<TEntity> DatabaseSet;

        protected RepositoryBase(DataContext context)
        {
            Context = context;
            DatabaseSet = context.Set<TEntity>();
        }

        public virtual TEntity GetById(object id)
        {
            return DatabaseSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DatabaseSet;
        }

        public virtual void Insert(TEntity entity)
        {
            DatabaseSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            DatabaseSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DatabaseSet.Attach(entity);
            }
            DatabaseSet.Remove(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entity = DatabaseSet.Find(id);
            if (entity != null)
                Delete(entity);
        }

        public virtual void Commit()
        {
            Context.SaveChanges();
        }

        public virtual void Dispose()
        {
            Context.Dispose();
        }
    }
}
