using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Generic.Repository.Contract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Dispose();
        Task DisposeAsync();
        void SetCommandTimeout(int timeout);

        void SetEntityState<TEntity>(TEntity entity, EntityState state) where TEntity : class;
        Task CommitAsync();
        Task SaveAsync();
        Task SaveAndCommitAsync();
        Task<TEntity> GetByIdAsync(object id);
        // TEntity? GetById(object id);
        // bool Insert(TEntity entity);
        Task<bool> InsertAsync(TEntity entity);
        // bool InsertRange(IEnumerable<TEntity> entities);
        Task<bool> InsertRangeAsync(IEnumerable<TEntity> entities);

        Task<bool> Delete(TEntity entityToDelete);
        Task<bool> Delete(object id);
        Task<bool> DeleteRange(IEnumerable<TEntity> entitiesToDelete);
        Task<bool> Update(TEntity entityToUpdate);
        Task<bool> UpdateRange(IEnumerable<TEntity> entitiesToUpdate);
        //IEnumerable<TEntity> Get(
        //    Expression<Func<TEntity, bool>> filter = null,
        //    Func<IQueryable<TEntity>,
        //        IOrderedQueryable<TEntity>> orderBy = null,
        //    string includeProperties = "");
        Task<(IEnumerable<TEntity> entites, int count)> GetPaging(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,
                IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int pageNumber = 0,
            int recordCount = 0);
        //Task<IEnumerable<TEntity>> GetAsync(
        //    Expression<Func<TEntity, bool>> filter = null,
        //    Func<IQueryable<TEntity>,
        //        IOrderedQueryable<TEntity>> orderBy = null,
        //    string includeProperties = "");
    }
}
