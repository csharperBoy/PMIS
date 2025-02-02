using Generic.Base.Handler.SystemException.Abstract;
using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.Repository.Abstract;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace Generic.Repository
{
    internal class GenericSqlServerWithDapperRepository<TEntity, TContext>  : AbstractGenericRepository<TEntity, TContext>, IDisposable
        where TEntity : class, new()
        where TContext : DbContext
    {
        private readonly IDbConnection dbConnection;
        private IDbTransaction transaction;
        private readonly AbstractGenericExceptionHandler exceptionHandler;
        private readonly Serilog.ILogger logHandler;
        private bool disposed;
        private readonly string entityName;

        public GenericSqlServerWithDapperRepository(string connectionString, AbstractGenericExceptionHandler _exceptionHandler, AbstractGenericLogWithSerilogHandler _logHandler)
        {
            dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();
            transaction = dbConnection.BeginTransaction();
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
                var sql = $"INSERT INTO {entityName} ({string.Join(", ", GetProperties(entity))}) VALUES ({string.Join(", ", GetPropertyValues(entity))})";
                await dbConnection.ExecuteAsync(sql, entity, transaction);
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
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            bool result;
            try
            {
                foreach (var entity in entities)
                {
                    var sql = $"INSERT INTO {entityName} ({string.Join(", ", GetProperties(entity))}) VALUES ({string.Join(", ", GetPropertyValues(entity))})";
                    await dbConnection.ExecuteAsync(sql, entity, transaction);
                }
                result = true;
            }
            catch (Exception)
            {
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
                var sql = $"UPDATE {entityName} SET {string.Join(", ", GetUpdateProperties(entity))} WHERE Id = @Id";
                await dbConnection.ExecuteAsync(sql, entity, transaction);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                var entityJson = Helper.Helper.Convert.ConvertObjectToJson(entity);
                logHandler.Information("Update {@entityJson} in {@entityName}", entityJson, entityName);
            }
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            bool result;
            try
            {
                foreach (var entity in entities)
                {
                    var sql = $"UPDATE {entityName} SET {string.Join(", ", GetUpdateProperties(entity))} WHERE Id = @Id";
                    await dbConnection.ExecuteAsync(sql, entity, transaction);
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
                logHandler.Information("UpdateRange {@entityJson} in {@entityName}", entityJson, entityName);
            }
            return result;
        }

        public override async Task<bool> DeleteAsync(TEntity entity)
        {
            bool result;
            try
            {
                var sql = $"DELETE FROM {entityName} WHERE Id = @Id";
                await dbConnection.ExecuteAsync(sql, new { Id = entity.GetType().GetProperty("Id")?.GetValue(entity) }, transaction);
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
            try
            {
                var sql = $"DELETE FROM {entityName} WHERE Id = @Id";
                await dbConnection.ExecuteAsync(sql, new { Id = id }, transaction);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                logHandler.Information("Delete by id {@id} in {@entityName}", id, entityName);
            }
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            bool result;
            try
            {
                foreach (var entity in entities)
                {
                    var sql = $"DELETE FROM {entityName} WHERE Id = @Id";
                    await dbConnection.ExecuteAsync(sql, new { Id = entity.GetType().GetProperty("Id")?.GetValue(entity) }, transaction);
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
                await dbConnection.ExecuteAsync("COMMIT", transaction: transaction);
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
                transaction.Commit();
                transaction = dbConnection.BeginTransaction();
            }
            catch (Exception)
            {
                await RollbackAsync();
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
                transaction.Rollback();
                transaction = dbConnection.BeginTransaction();
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

        public override async Task<TEntity> GetByIdAsync(object id)
        {
            try
            {
                var sql = $"SELECT * FROM {entityName} WHERE Id = @Id";
                var entity = await dbConnection.QueryFirstOrDefaultAsync<TEntity>(sql, new { Id = id }, transaction);
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override async Task<(IEnumerable<TEntity> entites, int count)> GetPagingAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int? pageNumber = null, int? recordCount = null)
        {
            try
            {
                var sql = $"SELECT * FROM {entityName}";
                if (pageNumber != null && recordCount != null)
                {
                    sql += $" ORDER BY Id OFFSET {(pageNumber - 1) * recordCount} ROWS FETCH NEXT {recordCount} ROWS ONLY";
                }
                var resultList = await dbConnection.QueryAsync<TEntity>(sql, transaction: transaction);
                var count = await dbConnection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM {entityName}", transaction: transaction);
                return (resultList, count);
            }
            catch (Exception)
            {
                return (null, -2);
            }
        }

        private IEnumerable<string> GetProperties(TEntity entity)
        {
            return entity.GetType().GetProperties().Where(p => p.Name != "Id").Select(p => p.Name);
        }

        private IEnumerable<string> GetPropertyValues(TEntity entity)
        {
            return entity.GetType().GetProperties().Where(p => p.Name != "Id").Select(p => $"@{p.Name}");
        }

        private IEnumerable<string> GetUpdateProperties(TEntity entity)
        {
            return entity.GetType().GetProperties().Where(p => p.Name != "Id").Select(p => $"{p.Name} = @{p.Name}");
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    transaction.Dispose();
                    dbConnection.Dispose();
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
                transaction.Dispose();
                dbConnection.Dispose();
                disposed = true;
            }
        }

        public override Task SetEntityStateAsync(TEntity entity, EntityState state)
        {
            return null;
        }

        public override Task SetEntitiesStateAsync(IEnumerable<TEntity> entities, EntityState state)
        {
            return null;
        }

        public override Task SetCommandTimeoutAsync(int timeout)
        {
            return null;
        }
    }
}