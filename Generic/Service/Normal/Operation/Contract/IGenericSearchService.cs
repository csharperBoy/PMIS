using Generic.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Normal.Operation.Contract
{
    public interface IGenericSearchService<TEntity, TEntitySearchResponseDto>
        where TEntity : class       
        where TEntitySearchResponseDto : class
    {
        Task<(bool, IEnumerable<TEntitySearchResponseDto>)> Search(GenericSearchRequestDto requestInput);

    }
}
