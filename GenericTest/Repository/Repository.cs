using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericTest.Repository
{
    public class Repository<TEntity, TContext> : IRepository<TEntity>, IDisposable
          where TEntity : class, new()
        where TContext : DbContext
    {
        private DbContext dbContext;
        private DbSet<TEntity> dbSet;
        public Repository(TContext _dbContext)
        {
            dbContext = _dbContext;
            dbSet = _dbContext.Set<TEntity>();
        }
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

   
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async Task<bool> InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            bool result;
            try
            {
                await dbSet.AddRangeAsync(entities);
                //   dbContext.Entry(entities).State = EntityState.Added;
                await dbContext.SaveChangesAsync();
                result = true;
            }
            catch (Exception)
            {
                
                result = false;
            }
            
            return result;
        }
    }
}
