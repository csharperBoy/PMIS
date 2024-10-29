using Generic.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Abstract
{
    public abstract class AbstractGenericNormalEditService<TEntity, TEntityEditRequestDto, TEntityEditResponseDto> : IGenericEditService<TEntity, TEntityEditRequestDto, TEntityEditResponseDto>
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
