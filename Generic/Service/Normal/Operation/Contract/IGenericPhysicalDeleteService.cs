using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Normal.Operation.Contract
{
    public interface IGenericPhysicalDeleteService<TEntity, TEntityDeleteRequestDto, TEntityDeleteResponseDto>
        where TEntity : class
        where TEntityDeleteRequestDto : class
        where TEntityDeleteResponseDto : class
    {
        Task<(bool, IEnumerable<TEntityDeleteResponseDto>)> PhysicalDeleteGroup(IEnumerable<TEntityDeleteRequestDto> requestInput);
        Task<bool> PhysicalDeleteRange(IEnumerable<TEntityDeleteRequestDto> requestInput);
    }
}
