using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Normal.Operation.Contract
{
    public interface IGenericEditService<TEntity, TEntityEditRequestDto, TEntityEditResponseDto>
        where TEntity : class
        where TEntityEditRequestDto : class
        where TEntityEditResponseDto : class
    {
        Task<(bool, IEnumerable<TEntityEditResponseDto>)> EditGroup(IEnumerable<TEntityEditRequestDto> requestInput);
        Task<bool> EditRange(IEnumerable<TEntityEditRequestDto> requestInput);
    }
}
