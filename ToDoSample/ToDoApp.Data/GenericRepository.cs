using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace ToDoApp.Data
{
    public abstract class GenericRepository<TEntity, TKey> where TEntity : class
    {
        protected ToDoAppContext Context;
        protected DbSet<TEntity> dbSet;

        protected GenericRepository(ToDoAppContext context)
        {
            Context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetByKeyAsync(TKey pk)
        {
            return await dbSet.FindAsync(pk);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        public virtual void Delete(TKey pk)
        {
            TEntity entityToDelete = dbSet.Find(pk);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entitiesToUpdate)
        {
            dbSet.AttachRange(entitiesToUpdate);
            Context.Entry(entitiesToUpdate).State = EntityState.Modified;
        }
       
        public virtual IQueryable<TEntity> Get(Predicate<TEntity> predicate)
        {
            return dbSet.Where(x => predicate(x));
        }

    }
}
