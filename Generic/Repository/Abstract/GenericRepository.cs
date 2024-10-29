using Generic.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Generic.Repository.Abstract
{
    public abstract class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>, IDisposable
    where TEntity : class
    where TContext : DbContext
    {
        public abstract Task CommitAsync();

        public abstract bool Delete(TEntity entityToDelete);

        public abstract Task<bool> Delete(object id);

        public abstract bool DeleteRange(IEnumerable<TEntity> entitiesToDelete);

        public abstract void Dispose();

        public abstract Task DisposeAsync();

        public abstract Task<TEntity> GetByIdAsync(object id);

        public abstract Task<(IEnumerable<TEntity> entites, int count)> GetPaging(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int pageNumber = 0, int recordCount = 0);

        public abstract Task<bool> InsertAsync(TEntity entity);

        public abstract Task<bool> InsertRangeAsync(IEnumerable<TEntity> entities);

        public abstract Task SaveAndCommitAsync();

        public abstract Task SaveAsync();

        public abstract void SetCommandTimeout(int timeout);

        public abstract void SetEntityState<TEntity1>(TEntity1 entity, EntityState state) where TEntity1 : class;

        public abstract bool Update(TEntity entityToUpdate);

        public abstract bool UpdateRange(IEnumerable<TEntity> entitiesToUpdate);
    }
}
