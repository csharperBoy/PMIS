using Generic.Service.Normal.Operation.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Generic.Service.Normal.Operation
{
    internal class GenericNormalEditService<TContext, TEntity, TEntityEditRequestDto, TEntityEditResponseDto> : AbstractGenericNormalEditService<TContext, TEntity, TEntityEditRequestDto, TEntityEditResponseDto>
        where TContext : DbContext
        where TEntity : class
        where TEntityEditRequestDto : class
        where TEntityEditResponseDto : class
    {
       
    }
}