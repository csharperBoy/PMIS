using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Normal.Operation.Contract
{
    public interface IGenericLogicalDeleteService<TEntity, TEntityDeleteRequestDto, TEntityDeleteResponseDto>
        where TEntity : class
        where TEntityDeleteRequestDto : class
        where TEntityDeleteResponseDto : class
    {
        Task<(bool, IEnumerable<TEntityDeleteResponseDto>)> LogicalDeleteGroup(IEnumerable<TEntityDeleteRequestDto> requestInput);
        Task<(bool, IEnumerable<TEntityDeleteResponseDto>)> RecycleGroup(IEnumerable<TEntityDeleteRequestDto> requestInput);

        Task<bool> LogicalDeleteRange(IEnumerable<TEntityDeleteRequestDto> requestInput);
        Task<bool> RecycleRange(IEnumerable<TEntityDeleteRequestDto> requestInput);
    }
}
