using Generic.Base.Handler.Map;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.Base.Handler.SystemLog.WithSerilog.Concrete;
using Generic.Helper;
using Generic.Repository.Abstract;
using Generic.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Generic.Base.Handler.Map.GenericExceptionHandlerFactory;
using Helper = Generic.Helper;
namespace Generic.Repository
{
    public class GenericSqlServerRepository<TEntity, TContext> : AbstractGenericRepository<TEntity, TContext>, IDisposable
        where TEntity : class, new()
        where TContext : DbContext
    {
        private DbContext dbContext;
        private DbSet<TEntity> dbSet;
        private IDbContextTransaction transaction;
        private AbstractGenericExceptionHandler exceptionHandler;
        private Serilog.ILogger logHandler;
        private bool disposed;
        private string entityName;

        public GenericSqlServerRepository(TContext _dbContext, AbstractGenericExceptionHandler _exceptionHandler, AbstractGenericLogWithSerilogHandler _logHandler)
        {
            dbContext = _dbContext;
            dbSet = _dbContext.Set<TEntity>();
            transaction = _dbContext.Database.BeginTransaction();
            exceptionHandler = _exceptionHandler;
            logHandler = _logHandler.CreateLogger();
            disposed = false;
            entityName = typeof(TEntity).Name;
        }

        public override async Task<bool> InsertAsync(TEntity entity)
        {
            bool result;
            try
            {
                await dbSet.AddAsync(entity);

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                var entityJson = Helper.Helper.Convert.ConvertObjectToJson(entity);
                logHandler.Information("Add {@entityJson} in {@entityName}", entityJson, entityName);
            }
            return await Task.FromResult(result);
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            bool result;
            try
            {
                await dbSet.AddRangeAsync(entities);
             //   dbContext.Entry(entities).State = EntityState.Added;
                result = true;
            }
            catch (Exception)
            {
                //foreach (var item in entities)
                //{
                //    dbContext.Entry(item).State = EntityState.Detached;
                //}
                await RollbackAsync();
                result = false;
            }
            finally
            {
                var entityJson = Helper.Helper.Convert.ConvertObjectToJson(entities);
                logHandler.Information("Add range {@entityJson} in {@entityName}", entityJson, entityName);
            }
            return result;
        }

        public override async Task<bool> UpdateAsync(TEntity entity)
        {
            bool result;
            try
            {
                dbSet.Attach(entity);
                dbContext.Entry(entity).State = EntityState.Modified;

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                throw new NotImplementedException();

            }
            finally
            {
                var entityJson = Helper.Helper.Convert.ConvertObjectToJson(entity);
                logHandler.Information("Update {@entityJson} in {@entityName}", entityJson, entityName);
            }
            return await Task.FromResult(result);
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            bool result;
            try
            {
                dbSet.UpdateRange(entities);
                //foreach (var item in entities)
                //{
                //    await Task.FromResult(UpdateAsync(item));
                //}

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                var entityJson = Helper.Helper.Convert.ConvertObjectToJson(entities);
                logHandler.Information("UpdateRange {@entityJson} in {@entityName}", entityJson, entityName);
            }
            return await Task.FromResult(result);
        }

        public override async Task<bool> DeleteAsync(TEntity entity)
        {
            bool result;
            try
            {
                if (dbContext.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                    await Task.FromResult(dbSet.Remove(entity));
                }

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                var entityJson = Helper.Helper.Convert.ConvertObjectToJson(entity);
                logHandler.Information("Delete by entity {@entityJson} in {@entityName}", entityJson, entityName);
            }
            return result;
        }

        public override async Task<bool> DeleteAsync(object id)
        {
            bool result;
            TEntity? entity = null;
            try
            {
                entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    await DeleteAsync(entity);
                }
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                var entityJson = Helper.Helper.Convert.ConvertObjectToJson(entity);
                logHandler.Information("Delete by id {@entityJson} in {@entityName}", entityJson, entityName);
            }
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            bool result;
            try
            {
                //dbSet.RemoveRange(entities);
                foreach (var item in entities)
                {
                    await Task.FromResult(DeleteAsync(item));
                }

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                var entityJson = Helper.Helper.Convert.ConvertObjectToJson(entities);
                logHandler.Information("DeleteRange {@entityJson} in {@entityName}", entityJson, entityName);
            }
            return result;
        }

        public override async Task SaveAsync()
        {
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                await RollbackAsync();
                throw;
            }
        }

        public override async Task CommitAsync()
        {
            try
            {
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                transaction = dbContext.Database.BeginTransaction(); // Restart transaction after commit
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {

                logHandler.Information("Commited in {@entityName}", entityName);
            }
        }

        public override async Task SaveAndCommitAsync()
        {
            try
            {
                await SaveAsync();
                await CommitAsync();
            }
            catch (Exception)
            {
                await RollbackAsync();
                throw;
            }
        }

        public override async Task RollbackAsync()
        {
            try
            {
                await transaction.RollbackAsync();
                transaction = dbContext.Database.BeginTransaction(); // Restart transaction after rollback
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                logHandler.Information("Rollback in {@entityName}", entityName);
            }
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    transaction.Dispose();
                    dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override async Task DisposeAsync()
        {
            if (!disposed)
            {
                await transaction.DisposeAsync();
                await dbContext.DisposeAsync();
                disposed = true;
            }
        }

        public override Task SetEntityStateAsync(TEntity entity, EntityState state)
        {
            dbContext.Entry(entity).State = state;
            return Task.CompletedTask;
        }
        public override Task SetEntitiesStateAsync(IEnumerable<TEntity> entities, EntityState state)
        {
            foreach (var entity in entities)
            {
                SetEntityStateAsync(entity, state);
            }
            return Task.CompletedTask;
        }
        public override Task SetCommandTimeoutAsync(int timeout)
        {
            try
            {
                dbContext.Database.SetCommandTimeout(timeout);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public override async Task<TEntity> GetByIdAsync(object id)
        {
            try
            {
                TEntity? entity = new TEntity();
                entity = await dbSet.FindAsync(id);
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
            }
        }

        public override async Task<(IEnumerable<TEntity> entites, int count)> GetPagingAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int? pageNumber = null, int? recordCount = null)
        {
            try
            {
                IQueryable<TEntity> query = dbSet;
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                int count = await query.CountAsync();
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
                if (orderBy != null)
                {
                    query = orderBy(query).AsQueryable();
                }
                if (pageNumber != null && recordCount != null)
                {
                    int skip = ((int)pageNumber - 1) * (int)recordCount;
                    query = query.Skip(skip).Take((int)recordCount);
                }
                var resultList = await query.ToListAsync();
                return (resultList, count);
            }
            catch (Exception ex)
            {
                return (null, -2);
            }
            finally
            {
                // await CommitAsync();
            }
        }
    }
}

//using Generic.Repository.Abstract;
//using Generic.Repository.Contract;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Storage;
//using System.Linq.Expressions;

//namespace Generic.Repository
//{
//    public class GenericSqlServerRepository<TEntity, TContext> : AbstractGenericRepository<TEntity, TContext> ,IDisposable
//        where TEntity : class
//        where TContext : DbContext
//    {
//        protected DbContext dbContext;
//        internal DbSet<TEntity> dbSet;
//        private IDbContextTransaction transaction;
//        private bool disposed = false;
//        // protected readonly ILogger _logger;
//        public GenericSqlServerRepository(TContext dbContext)
//        {
//            this.dbContext = dbContext;
//            dbSet = dbContext.Set<TEntity>();
//            transaction = dbContext.Database.BeginTransaction();
//            //  _logger = logger;
//        }

//        public override async Task<bool> InsertAsync(TEntity entity)
//        {
//            try
//            {
//                await dbSet.AddAsync(entity);
//                return true;
//            }
//            catch (Exception ex)
//            {

//                // // _logger.LogError(ex, "{Repo} All function error", typeof(GenericRepository<TEntity>));
//                return false;
//            }
//        }
//        public override void SetCommandTimeout(int timeout)
//        {
//            try
//            {
//                dbContext.Database.SetCommandTimeout(timeout);
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }

//        public override async Task CommitAsync()
//        {
//            try
//            {
//                //await dbContext.SaveChangesAsync(); // Save changes to the database
//                await transaction.CommitAsync(); // Commit the transaction
//            }
//            catch (Exception)
//            {
//                // Handle exceptions appropriately (logging, etc.)
//                await transaction.RollbackAsync(); // Roll back the transaction
//                throw; // Rethrow the exception
//            }
//        }
//        public override void SetEntityState<TEntity>(TEntity entity, EntityState state) where TEntity : class
//        {
//            dbContext.Entry(entity).State = state;
//        }

//        public async Task RollbackAsync()
//        {
//            try
//            {
//                await transaction.RollbackAsync(); // Roll back the transaction
//            }
//            catch (Exception)
//            {
//                // Handle exceptions appropriately (logging, etc.)
//                throw; // Rethrow the exception
//            }
//        }

//        // Dispose pattern implementation
//        public void Dispose(bool disposing)
//        {
//            if (!disposed)
//            {
//                if (disposing)
//                {
//                    transaction.Dispose(); // Dispose of the transaction                   
//                    dbContext.Dispose(); // Dispose of the DbContext
//                }
//            }
//            disposed = true;
//        }
//        public override async Task DisposeAsync()
//        {
//            if (!disposed)
//            {
//                await transaction.DisposeAsync(); // Dispose of the transaction                
//                await dbContext.DisposeAsync(); // Dispose of the DbContext
//                disposed = true;
//            }
//        }
//        public override void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//        public override async Task SaveAsync()
//        {
//            try
//            {
//                await dbContext.SaveChangesAsync(); // Save changes to the database                
//            }
//            catch (Exception)
//            {
//                // Handle exceptions appropriately (logging, etc.)
//                await transaction.RollbackAsync(); // Roll back the transaction
//                throw; // Rethrow the exception
//            }
//        }

//        public override async Task SaveAndCommitAsync()
//        {
//            try
//            {
//                await SaveAsync();
//                await CommitAsync();
//            }
//            catch (Exception ex)
//            {
//                await RollbackAsync();
//                throw;
//            }
//        }
//        //public override bool Insert(TEntity entity)
//        //{
//        //    try
//        //    {
//        //        dbSet.Add(entity);
//        //        return true;
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        // _logger.LogError(ex, "{Repo} All function error", typeof(GenericRepository<TEntity>));
//        //        return false;
//        //    }
//        //}
//        //public override bool InsertRange(IEnumerable<TEntity> entities)
//        //{
//        //    try
//        //    {
//        //        dbSet.AddRange(entities);
//        //        return true;
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        return false;
//        //    }
//        //}

//        public override async Task<bool> InsertRangeAsync(IEnumerable<TEntity> entities)
//        {
//            try
//            {
//                await dbSet.AddRangeAsync(entities);
//                return true;
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }
//        }
//        public override bool Delete(TEntity entityToDelete)
//        {
//            try
//            {
//                if (dbContext.Entry(entityToDelete).State == EntityState.Detached)
//                {
//                    dbSet.Attach(entityToDelete);
//                }
//                dbSet.Remove(entityToDelete);
//                return true;
//            }
//            catch (Exception ex)
//            {

//                // _logger.LogError(ex, "{Repo} All function error", typeof(GenericRepository<TEntity>));
//                return false;
//            }
//        }
//        public override async Task<bool> Delete(object id)
//        {
//            try
//            {
//                TEntity? entityToDelete = await GetByIdAsync(id);
//                if (entityToDelete != null)
//                {
//                    return Delete(entityToDelete);
//                }
//                return true;
//            }
//            catch (Exception ex)
//            {

//                // _logger.LogError(ex, "{Repo} All function error", typeof(GenericRepository<TEntity>));
//                return false;
//            }
//        }
//        public override bool DeleteRange(IEnumerable<TEntity> entitiesToDelete)
//        {
//            try
//            {
//                //if (dbContext.Entry(entitiesToDelete).State == EntityState.Detached)
//                //{
//                //    dbSet.AttachRange(entitiesToDelete);
//                //}
//                //dbSet.RemoveRange(entitiesToDelete);
//                dbContext.RemoveRange(entitiesToDelete);
//                return true;
//            }
//            catch (Exception ex)
//            {

//                // _logger.LogError(ex, "{Repo} All function error", typeof(GenericRepository<TEntity>));
//                return false;
//            }
//        }

//        public override async Task<TEntity?> GetByIdAsync(object id)
//        {
//            try
//            {
//                TEntity? entity = await dbSet.FindAsync(id);
//                return entity;
//            }
//            catch (Exception ex)
//            {

//                // _logger.LogError(ex, "{Repo} All function error", typeof(GenericRepository<TEntity>));
//                return null;
//            }
//        }
//        //public TEntity? GetById(object id)
//        //{
//        //    try
//        //    {
//        //        TEntity? entity = dbSet.Find(id);
//        //        return entity;
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        // _logger.LogError(ex, "{Repo} All function error", typeof(GenericRepository<TEntity>));
//        //        return null;
//        //    }
//        //}

//        //public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
//        //{
//        //    try
//        //    {
//        //        IQueryable<TEntity> query = dbSet;

//        //        if (filter != null)
//        //        {
//        //            query = query.Where(filter);
//        //        }

//        //        foreach (var includeProperty in includeProperties.Split
//        //            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
//        //        {
//        //            query = query.Include(includeProperty);
//        //        }

//        //        if (orderBy != null)
//        //        {
//        //            return orderBy(query).ToList();
//        //        }
//        //        else
//        //        {
//        //            return query.ToList();
//        //        }

//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        // _logger.LogError(ex, "{Repo} All function error", typeof(GenericRepository<TEntity>));
//        //        return null;
//        //    }
//        //}
//        //public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
//        //{
//        //    try
//        //    {
//        //        IQueryable<TEntity> query = dbSet;

//        //        if (filter != null)
//        //        {
//        //            query = query.Where(filter);
//        //        }

//        //        foreach (var includeProperty in includeProperties.Split
//        //            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
//        //        {
//        //            query = query.Include(includeProperty);
//        //        }

//        //        if (orderBy != null)
//        //        {
//        //            return await orderBy(query).ToListAsync();
//        //        }
//        //        else
//        //        {
//        //            return await query.ToListAsync();
//        //        }

//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        // _logger.LogError(ex, "{Repo} All function error", typeof(GenericRepository<TEntity>));
//        //        return null;
//        //    }
//        //}

//        public override bool Update(TEntity entityToUpdate)
//        {
//            try
//            {

//                dbSet.Attach(entityToUpdate);
//                dbContext.Entry(entityToUpdate).State = EntityState.Modified;
//                return true;
//            }
//            catch (Exception ex)
//            {

//                // _logger.LogError(ex, "{Repo} All function error", typeof(GenericRepository<TEntity>));
//                return false;
//            }
//        }
//        public override bool UpdateRange(IEnumerable<TEntity> entitiesToUpdate)
//        {
//            try
//            {
//                foreach (var item in entitiesToUpdate)
//                {
//                    Update(item);
//                }
//                return true;
//            }
//            catch (Exception ex)
//            {

//                // _logger.LogError(ex, "{Repo} All function error", typeof(GenericRepository<TEntity>));
//                return false;
//            }
//        }

//        public override async Task<(IEnumerable<TEntity> entites, int count)> GetPaging(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int pageNumber = 0, int recordCount = 0)
//        {
//            try
//            {
//                IQueryable<TEntity> query = dbSet;

//                if (filter != null)
//                {
//                    query = query.Where(filter);
//                }
//                int count = query.Count();
//                foreach (var includeProperty in includeProperties.Split
//                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
//                {
//                    query = query.Include(includeProperty);
//                }

//                if (orderBy != null)
//                {
//                    query = orderBy(query).AsQueryable();
//                }

//                if (pageNumber > 0 && recordCount > 0)
//                {
//                    int skip = (pageNumber - 1) * recordCount;
//                    query = query.Skip(skip).Take(recordCount);
//                }
//                var resultList = await query.ToListAsync();
//                return (resultList, count);

//            }
//            catch (Exception ex)
//            {

//                // _logger.LogError(ex, "{Repo} All function error", typeof(GenericRepository<TEntity>));
//                return (null, -2);
//            }
//        }
//    }
//}

#region old
//using Generic.Repository.Abstract;
//using Generic.Repository.Contract;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Storage;
//using System.Linq.Expressions;

//namespace Generic.Repository
//{
//    public class GenericSqlServerRepository<TEntity, TContext> : AbstractGenericRepository<TEntity, TContext>, IDisposable
//        where TEntity : class
//        where TContext : DbContext
//    {
//        protected DbContext dbContext;
//        internal DbSet<TEntity> dbSet;
//        private IDbContextTransaction transaction;
//        private bool disposed = false;
//        public GenericSqlServerRepository(TContext dbContext)
//        {
//            this.dbContext = dbContext;
//            dbSet = dbContext.Set<TEntity>();
//            transaction = dbContext.Database.BeginTransaction();

//        }

//        public override async Task<bool> InsertAsync(TEntity entity)
//        {
//            try
//            {
//                await dbSet.AddAsync(entity);
//                return true;
//            }
//            catch (Exception ex)
//            {

//                return false;
//            }
//        }
//        public override void SetCommandTimeout(int timeout)
//        {
//            try
//            {
//                dbContext.Database.SetCommandTimeout(timeout);
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }

//        public override async Task CommitAsync()
//        {
//            try
//            {
//                await transaction.CommitAsync();
//            }
//            catch (Exception)
//            {
//                await transaction.RollbackAsync();
//                throw;
//            }
//        }
//        public override void SetEntityState<TEntity>(TEntity entity, EntityState state) where TEntity : class
//        {
//            dbContext.Entry(entity).State = state;
//        }

//        public async Task RollbackAsync()
//        {
//            try
//            {
//                await transaction.RollbackAsync();
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        public void Dispose(bool disposing)
//        {
//            if (!disposed)
//            {
//                if (disposing)
//                {
//                    transaction.Dispose();
//                    dbContext.Dispose();
//                }
//            }
//            disposed = true;
//        }
//        public override async Task DisposeAsync()
//        {
//            if (!disposed)
//            {
//                await transaction.DisposeAsync();
//                await dbContext.DisposeAsync();
//                disposed = true;
//            }
//        }
//        public override void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//        public override async Task SaveAsync()
//        {
//            try
//            {
//                await dbContext.SaveChangesAsync();
//            }
//            catch (Exception)
//            {
//                await transaction.RollbackAsync();
//                throw;
//            }

//        public override async Task SaveAndCommitAsync()
//        {
//            try
//            {
//                await SaveAsync();
//                await CommitAsync();
//            }
//            catch (Exception ex)
//            {
//                await RollbackAsync();
//                throw;
//            }
//        }
//        public override async Task<bool> InsertRangeAsync(IEnumerable<TEntity> entities)
//        {
//            try
//            {
//                await dbSet.AddRangeAsync(entities);
//                return true;
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }
//        }
//        public override bool Delete(TEntity entityToDelete)
//        {
//            try
//            {
//                if (dbContext.Entry(entityToDelete).State == EntityState.Detached)
//                {
//                    dbSet.Attach(entityToDelete);
//                }
//                dbSet.Remove(entityToDelete);
//                return true;
//            }
//            catch (Exception ex)
//            {

//                return false;
//            }
//        }
//        public override async Task<bool> Delete(object id)
//        {
//            try
//            {
//                TEntity? entityToDelete = await GetByIdAsync(id);
//                if (entityToDelete != null)
//                {
//                    return Delete(entityToDelete);
//                }
//                return true;
//            }
//            catch (Exception ex)
//            {

//                return false;
//            }
//        }
//        public override bool DeleteRange(IEnumerable<TEntity> entitiesToDelete)
//        {
//            try
//            {
//                dbContext.RemoveRange(entitiesToDelete);
//                return true;
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }
//        }

//        public override async Task<TEntity?> GetByIdAsync(object id)
//        {
//            try
//            {
//                TEntity? entity = await dbSet.FindAsync(id);
//                return entity;
//            }
//            catch (Exception ex)
//            {

//                return null;
//            }
//        }

//        public override bool Update(TEntity entityToUpdate)
//        {
//            try
//            {

//                dbSet.Attach(entityToUpdate);
//                dbContext.Entry(entityToUpdate).State = EntityState.Modified;
//                return true;
//            }
//            catch (Exception ex)
//            {

//                return false;
//            }
//        }
//        public override bool UpdateRange(IEnumerable<TEntity> entitiesToUpdate)
//        {
//            try
//            {
//                foreach (var item in entitiesToUpdate)
//                {
//                    Update(item);
//                }
//                return true;
//            }
//            catch (Exception ex)
//            {

//                return false;
//            }
//        }

//        public override async Task<(IEnumerable<TEntity> entites, int count)> GetPaging(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int pageNumber = 0, int recordCount = 0)
//        {
//            try
//            {
//                IQueryable<TEntity> query = dbSet;

//                if (filter != null)
//                {
//                    query = query.Where(filter);
//                }
//                int count = query.Count();
//                foreach (var includeProperty in includeProperties.Split
//                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
//                {
//                    query = query.Include(includeProperty);
//                }

//                if (orderBy != null)
//                {
//                    query = orderBy(query).AsQueryable();
//                }

//                if (pageNumber > 0 && recordCount > 0)
//                {
//                    int skip = (pageNumber - 1) * recordCount;
//                    query = query.Skip(skip).Take(recordCount);
//                }
//                var resultList = await query.ToListAsync();
//                return (resultList, count);

//            }
//            catch (Exception ex)
//            {

//                return (null, -2);
//            }
//        }
//    }
//}

#endregion