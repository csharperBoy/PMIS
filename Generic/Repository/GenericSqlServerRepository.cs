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
using Helper = Generic.Helper;
namespace Generic.Repository
{
    public class GenericSqlServerRepository<TEntity, TContext> : AbstractGenericRepository<TEntity, TContext>, IDisposable
        where TEntity : class
        where TContext : DbContext
    {
        protected DbContext dbContext;
        internal DbSet<TEntity> dbSet;
        private IDbContextTransaction transaction;
        private bool disposed = false;
        private Serilog.ILogger logHandler;
        private string entityName = typeof(TEntity).Name;

        public GenericSqlServerRepository(TContext dbContext, AbstractGenericLogWithSerilogHandler _logHandler)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
            transaction = dbContext.Database.BeginTransaction();
            logHandler = _logHandler.CreateLogger();
        }

        public override async Task<bool> InsertAsync(TEntity entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                var entityJson = Helper.Helper.Convert.ConvertObjectToJson(entity);
                logHandler.Information("Add {@entityJson} in {@entityName}", entityJson, entityName);
            }
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

                logHandler.Information("Commited in {@entityName}",  entityName);
            }
        }

        public override Task SetEntityStateAsync<TEntity>(TEntity entity, EntityState state)
        {
            dbContext.Entry(entity).State = state;
            return Task.CompletedTask;
        }

        public async Task RollbackAsync()
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
                logHandler.Information("Rollback in {@entityName}",  entityName);
            }
        }

        public void Dispose(bool disposing)
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

        public override async Task DisposeAsync()
        {
            if (!disposed)
            {
                await transaction.DisposeAsync();
                await dbContext.DisposeAsync();
                disposed = true;
            }
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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

        public override async Task<bool> InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                await dbSet.AddRangeAsync(entities);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                var entityJson = Helper.Helper.Convert.ConvertObjectToJson(entities);
                logHandler.Information("Add range {@entityJson} in {@entityName}", entityJson, entityName);
            }
        }

        public override Task<bool> DeleteAsync(TEntity entityToDelete)
        {
            try
            {
                if (dbContext.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
            finally
            {
                var entityJson = Helper.Helper.Convert.ConvertObjectToJson(entityToDelete);
                logHandler.Information("Delete by entity {@entityJson} in {@entityName}", entityJson, entityName);
            }
        }

        public override async Task<bool> DeleteAsync(object id)
        {
            TEntity? entityToDelete = null;
            try
            {
                entityToDelete = await GetByIdAsync(id);
                if (entityToDelete != null)
                {
                    return await DeleteAsync(entityToDelete);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                var entityJson = Helper.Helper.Convert.ConvertObjectToJson(entityToDelete);
                logHandler.Information("Delete by id {@entityJson} in {@entityName}", entityJson, entityName);
            }
        }

        public override Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entitiesToDelete)
        {
            try
            {
                dbSet.RemoveRange(entitiesToDelete);
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
            finally
            {
                var entityJson = Helper.Helper.Convert.ConvertObjectToJson(entitiesToDelete);
                logHandler.Information("DeleteRange {@entityJson} in {@entityName}", entityJson, entityName);
            }
        }

        public override async Task<TEntity?> GetByIdAsync(object id)
        {
            try
            {
                TEntity entity = await dbSet.FindAsync(id);
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

        public override Task<bool> UpdateAsync(TEntity entityToUpdate)
        {
            try
            {
                dbSet.Attach(entityToUpdate);
                dbContext.Entry(entityToUpdate).State = EntityState.Modified;
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
            finally
            {
                var entityJson = Helper.Helper.Convert.ConvertObjectToJson(entityToUpdate);
                logHandler.Information("Update {@entityJson} in {@entityName}", entityJson, entityName);
            }
        }

        public  override async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entitiesToUpdate)
        {
            try
            {
                foreach (var item in entitiesToUpdate)
                {
                    await UpdateAsync(item);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                var entityJson = Helper.Helper.Convert.ConvertObjectToJson(entitiesToUpdate);
                logHandler.Information("UpdateRange {@entityJson} in {@entityName}", entityJson, entityName);
            }
        }

        public override async Task<(IEnumerable<TEntity> entites, int count)> GetPagingAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int pageNumber = 0, int recordCount = 0)
        {
            try
            {
                IQueryable<TEntity> query = dbSet;
                if (filter != null)
                {
                    query = query.Where(filter);
                }
               int count =await query.CountAsync();
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
                if (orderBy != null)
                {
                    query = orderBy(query).AsQueryable();
                }
                if (pageNumber > 0 && recordCount > 0)
                {
                    int skip = (pageNumber - 1) * recordCount;
                    query = query.Skip(skip).Take(recordCount);
                }
                var resultList = await query.ToListAsync();
                return (resultList, count);
            }
            catch (Exception ex)
            {
                return (null, -2);
            }
        }
    }
}




