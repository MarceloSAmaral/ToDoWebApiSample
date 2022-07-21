using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.CoreObjects.Entities;

namespace ToDoApp.CoreObjects.RepoInterfaces
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByKeyAsync(TKey pk);
        Task InsertAsync(TEntity entity);
        Task InsertRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entityToUpdate);
        void UpdateRange(IEnumerable<TEntity> entitiesToUpdate);
    }

    public interface IToDoItemsRepository : IGenericRepository<ToDoItem, Guid>
    {

    }
}
