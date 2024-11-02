using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Generic.Repository.Contract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Dispose();
        Task DisposeAsync();
        Task SetCommandTimeoutAsync(int timeout);

        Task SetEntityStateAsync<TEntity>(TEntity entity, EntityState state) where TEntity : class;
        Task CommitAsync();
        Task SaveAsync();
        Task SaveAndCommitAsync();
        Task<TEntity> GetByIdAsync(object id);
       
        Task<bool> InsertAsync(TEntity entity);
        Task<bool> InsertRangeAsync(IEnumerable<TEntity> entities);

        Task<bool> DeleteAsync(TEntity entityToDelete);
        Task<bool> DeleteAsync(object id);
        Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entitiesToDelete);
        Task<bool> UpdateAsync(TEntity entityToUpdate);
        Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entitiesToUpdate);
        
        Task<(IEnumerable<TEntity> entites, int count)> GetPagingAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,
                IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int pageNumber = 0,
            int recordCount = 0);
    }
}
