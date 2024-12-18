using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericTest.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<bool> InsertRangeAsync(IEnumerable<TEntity> entities);
    }
}
