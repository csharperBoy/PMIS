using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Contract
{
    public interface IGenericPhisycalDeleteService<TEntity, TEntityPhisycalDeleteRequestDto, TEntityPhisycalDeleteResponseDto>
        where TEntity : class
        where TEntityPhisycalDeleteRequestDto : class
        where TEntityPhisycalDeleteResponseDto : class
    {
        Task<(bool, IEnumerable<TEntityPhisycalDeleteResponseDto>)> PhisycalDeleteGroup(IEnumerable<TEntityPhisycalDeleteRequestDto> requestInput);
    }
}
