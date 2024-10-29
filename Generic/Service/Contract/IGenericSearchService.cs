using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Contract
{
    public interface IGenericSearchService<TEntity, TEntitySearchRequestDto, TEntitySearchResponseDto>
        where TEntity : class
        where TEntitySearchRequestDto : class
        where TEntitySearchResponseDto : class
    {
        Task<(bool, IEnumerable<TEntitySearchResponseDto>)> Search(IEnumerable<TEntitySearchRequestDto> requestInput);
        Task<(bool, IEnumerable<TEntitySearchResponseDto>)> Fetch(IEnumerable<TEntitySearchRequestDto> requestInput);
        Task<(bool, IEnumerable<TEntitySearchResponseDto>)> Translate(IEnumerable<TEntitySearchRequestDto> requestInput);
    }
}
