using Generic.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Repository
{
    public class GenericSqlServerRepository<TEntity, TContext> : AbstractGenericSqlServerRepository<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        public GenericSqlServerRepository(TContext dbContext) : base(dbContext)
        {
        }
    }
}
