﻿using Generic.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Generic.Repository.Abstract
{
    public abstract class AbstractGenericRepository<TEntity, TContext> : IGenericRepository<TEntity>, IDisposable
    where TEntity : class
    where TContext : DbContext
    {
        public abstract Task<bool> InsertAsync(TEntity entity);

        public abstract Task<bool> InsertRangeAsync(IEnumerable<TEntity> entities);

        public abstract Task<bool> UpdateAsync(TEntity entityToUpdate);

        public abstract Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entitiesToUpdate);

        public abstract Task<bool> DeleteAsync(TEntity entityToDelete);

        public abstract Task<bool> DeleteAsync(object id);

        public abstract Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entitiesToDelete);

        public abstract Task SaveAsync();

        public abstract Task CommitAsync();

        public abstract Task SaveAndCommitAsync();

        public abstract Task RollbackAsync();

        public abstract void Dispose();

        public abstract Task DisposeAsync();

        public abstract Task SetEntityStateAsync(TEntity entity, EntityState state);

        public abstract Task SetEntitiesStateAsync(IEnumerable<TEntity> entities, EntityState state);

        public abstract Task SetCommandTimeoutAsync(int timeout);

        public abstract Task<TEntity> GetByIdAsync(object id);

        public abstract Task<(IEnumerable<TEntity> entites, int count)> GetPagingAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int? pageNumber = null, int? recordCount = null);
    }
}
