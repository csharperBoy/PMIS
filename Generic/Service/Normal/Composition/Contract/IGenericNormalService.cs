using Generic.Service.Normal.Operation.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Normal.Composition.Contract
{
    public interface IGenericNormalService< TEntity, TEntityAddRequestDto, TEntityAddResponseDto, TEntityEditRequestDto, TEntityEditResponseDto, TEntityDeleteRequestDto, TEntityDeleteResponseDto, TEntitySearchResponseDto>
        : IGenericAddService<TEntity, TEntityAddRequestDto, TEntityAddResponseDto>,
    IGenericEditService<TEntity, TEntityEditRequestDto, TEntityEditResponseDto>,
    IGenericLogicalDeleteService<TEntity, TEntityDeleteRequestDto, TEntityDeleteResponseDto>,
    IGenericPhysicalDeleteService<TEntity, TEntityDeleteRequestDto, TEntityDeleteResponseDto>,
    IGenericSearchService<TEntity, TEntitySearchResponseDto>
    where TEntity : class
    where TEntityAddRequestDto : class
    where TEntityAddResponseDto : class
    where TEntityEditRequestDto : class
    where TEntityEditResponseDto : class
    where TEntityDeleteRequestDto : class
    where TEntityDeleteResponseDto : class
    where TEntitySearchResponseDto : class
    {
    }
}
