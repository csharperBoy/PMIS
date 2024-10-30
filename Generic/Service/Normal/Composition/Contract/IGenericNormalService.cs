using Generic.Service.Normal.Operation.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Normal.Composition.Contract
{
    public interface IGenericNormalService<TEntity, TEntityAddRequestDto, TEntityAddResponseDto,TEntityEditRequestDto, TEntityEditResponseDto>
        : IGenericAddService<TEntity, TEntityAddRequestDto, TEntityAddResponseDto>
        , IGenericEditService<TEntity, TEntityEditRequestDto, TEntityEditResponseDto>
        where TEntity : class
         where TEntityAddRequestDto : class
        where TEntityAddResponseDto : class
        where TEntityEditRequestDto : class
        where TEntityEditResponseDto : class
    {
    }
}
