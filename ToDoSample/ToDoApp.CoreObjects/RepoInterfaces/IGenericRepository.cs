using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoApp.CoreObjects.RepoInterfaces
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByKeyAsync(TKey pk);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void Update(TEntity entityToUpdate);
        void UpdateRange(IEnumerable<TEntity> entitiesToUpdate);
        void Delete(TKey key);
    }
}
