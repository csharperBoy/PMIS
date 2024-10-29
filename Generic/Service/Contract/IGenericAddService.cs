using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Contract
{
    public interface IGenericAddService<TEntity, TEntityAddRequestDto, TEntityAddResponseDto>
        where TEntity : class
        where TEntityAddRequestDto : class
        where TEntityAddResponseDto : class
    {
        Task<(bool, IEnumerable<TEntityAddResponseDto>)> AddGroup(IEnumerable<TEntityAddRequestDto> requestInput);
        Task<bool> AddRange(IEnumerable<TEntityAddRequestDto> requestInput);
    }
}
