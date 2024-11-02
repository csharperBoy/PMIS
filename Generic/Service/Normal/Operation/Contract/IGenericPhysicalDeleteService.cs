using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Normal.Operation.Contract
{
    public interface IGenericPhisycalDeleteService<TEntity, TEntityDeleteRequestDto, TEntityDeleteResponseDto>
        where TEntity : class
        where TEntityDeleteRequestDto : class
        where TEntityDeleteResponseDto : class
    {
        Task<(bool, IEnumerable<TEntityDeleteResponseDto>)> PhisycalDeleteGroup(IEnumerable<TEntityDeleteRequestDto> requestInput);
        Task<bool> PhisycalDeleteRange(IEnumerable<TEntityDeleteRequestDto> requestInput);
    }
}
