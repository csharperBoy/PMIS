using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        bool Delete(TEntity entityToDelete);
        Task<bool> Delete(object id);
        bool DeleteRange(IEnumerable<TEntity> entitiesToDelete);
        bool Update(TEntity entityToUpdate);
        bool UpdateRange(IEnumerable<TEntity> entitiesToUpdate);
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
