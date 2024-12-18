using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericTest.Repository
{
    public abstract class ARepository<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto>
        : IRepository<TEntity, TEntityAddRequestDto, TEntityAddResponseDto>
    {
    }
}
