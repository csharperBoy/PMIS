using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Normal.Operation.Contract
{
    public interface IGenericLogicalDeleteService<TEntity, TEntityLogicalDeleteRequestDto, TEntityLogicalDeleteResponseDto>
        where TEntity : class
        where TEntityLogicalDeleteRequestDto : class
        where TEntityLogicalDeleteResponseDto : class
    {
        Task<(bool, IEnumerable<TEntityLogicalDeleteResponseDto>)> LogicalDeleteGroup(IEnumerable<TEntityLogicalDeleteRequestDto> requestInput);
        Task<(bool, IEnumerable<TEntityLogicalDeleteResponseDto>)> RecycleGroup(IEnumerable<TEntityLogicalDeleteRequestDto> requestInput);

        Task<bool> LogicalDeleteRange(IEnumerable<TEntityLogicalDeleteRequestDto> requestInput);
        Task<bool> RecycleRange(IEnumerable<TEntityLogicalDeleteRequestDto> requestInput);
    }
}
