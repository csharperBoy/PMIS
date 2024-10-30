using Generic.Base.Handler.Map;
using Generic.Service.Normal.Operation.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Normal.Operation.Abstract
{
    public abstract class AbstractGenericNormalEditService<TContext, TEntity, TEntityEditRequestDto, TEntityEditResponseDto>
        : IGenericEditService<TEntity, TEntityEditRequestDto, TEntityEditResponseDto>
        where TContext : DbContext
        where TEntity : class
        where TEntityEditRequestDto : class
        where TEntityEditResponseDto : class
    {
        public Task<(bool, IEnumerable<TEntityEditResponseDto>)> EditGroup(IEnumerable<TEntityEditRequestDto> requestInput)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditRange(IEnumerable<TEntityEditRequestDto> requestInput)
        {
            throw new NotImplementedException();
        }
    }
}
