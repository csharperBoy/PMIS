using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Contract
{
    public interface IGenericService<TEntity,
                                     TEntityAddRequestDto,
                                     TEntityAddResponseDto,
                                     TEntityEditRequestDto,
                                     TEntityEditResponseDto,
                                     TEntityLogicalDeleteRequestDto,
                                     TEntityLogicalDeleteResponseDto,
                                     TEntityPhisycalDeleteRequestDto,
                                     TEntityPhisycalDeleteResponseDto,
                                     TEntitySearchRequestDto,
                                     TEntitySearchResponseDto> :
        IGenericAddService<TEntity, TEntityAddRequestDto, TEntityAddResponseDto>,
        IGenericEditService<TEntity, TEntityEditRequestDto, TEntityEditResponseDto>,
        IGenericLogicalDeleteService<TEntity, TEntityLogicalDeleteRequestDto, TEntityLogicalDeleteResponseDto>,
        IGenericPhisycalDeleteService<TEntity, TEntityPhisycalDeleteRequestDto, TEntityPhisycalDeleteResponseDto>,
        IGenericSearchService<TEntity, TEntitySearchRequestDto, TEntitySearchResponseDto>

        where TEntity : class
        where TEntityAddRequestDto : class
        where TEntityAddResponseDto : class
        where TEntityEditRequestDto : class
        where TEntityEditResponseDto : class
        where TEntityLogicalDeleteRequestDto : class
        where TEntityLogicalDeleteResponseDto : class
        where TEntityPhisycalDeleteRequestDto : class
        where TEntityPhisycalDeleteResponseDto : class
        where TEntitySearchRequestDto : class
        where TEntitySearchResponseDto : class
    {
        Task<(bool, IEnumerable<TEntityAddResponseDto>)> AddGroup(IEnumerable<TEntityAddRequestDto> requestInput);
        Task<bool> AddRange(IEnumerable<TEntityAddRequestDto> reqEntities);
        Task<(bool, IEnumerable<TEntityEditResponseDto>)> EditGroup(IEnumerable<TEntityEditRequestDto> requestInput);
        Task<bool> EditRange(IEnumerable<TEntityEditRequestDto> reqEntities);
        Task<(bool, IEnumerable<TEntityLogicalDeleteResponseDto>)> LogicalDeleteGroup(IEnumerable<TEntityLogicalDeleteRequestDto> requestInput);
        Task<(bool, IEnumerable<TEntityLogicalDeleteResponseDto>)> RecycleGroup(IEnumerable<TEntityLogicalDeleteRequestDto> requestInput);
        Task<(bool, IEnumerable<TEntityPhisycalDeleteResponseDto>)> PhisycalDeleteGroup(IEnumerable<TEntityPhisycalDeleteRequestDto> requestInput);
        Task<(bool, IEnumerable<TEntitySearchResponseDto>)> Search(IEnumerable<TEntitySearchRequestDto> requestInput);
        
       
       
        
    }

}
