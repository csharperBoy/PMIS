using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Normal.Operation.Contract
{
    public interface IGenericSearchService<TEntity, TEntitySearchRequestDto, TEntitySearchResponseDto>
        where TEntity : class
        where TEntitySearchRequestDto : class
        where TEntitySearchResponseDto : class
    {
        Task<(bool, IEnumerable<TEntitySearchResponseDto>)> Search(IEnumerable<TEntitySearchRequestDto> requestInput);

    }
}
