using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Generic.Repository.Contract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<bool> InsertAsync(TEntity entity);

        Task<bool> InsertRangeAsync(IEnumerable<TEntity> entities);

        Task<bool> UpdateAsync(TEntity entityToUpdate);

        Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entitiesToUpdate);

        Task<bool> DeleteAsync(TEntity entityToDelete);

        Task<bool> DeleteAsync(object id);

        Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entitiesToDelete);

        Task SaveAsync();

        Task CommitAsync();

        Task SaveAndCommitAsync();

        Task RollbackAsync();

        void Dispose();

        Task DisposeAsync();

        Task SetEntityStateAsync(TEntity entity, EntityState state);

        Task SetCommandTimeoutAsync(int timeout);

        Task<TEntity> GetByIdAsync(object id);

        Task<(IEnumerable<TEntity> entites, int count)> GetPagingAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int? pageNumber = null, int? recordCount = null);
    }
}
