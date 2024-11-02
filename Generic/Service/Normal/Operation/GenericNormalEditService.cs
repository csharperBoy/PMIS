using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.Repository.Abstract;
using Generic.Service.Normal.Operation.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Generic.Service.Normal.Operation
{
    public class GenericNormalEditService<TContext, TEntity, TEntityEditRequestDto, TEntityEditResponseDto> : AbstractGenericNormalEditService<TContext, TEntity, TEntityEditRequestDto, TEntityEditResponseDto>
        where TContext : DbContext
        where TEntity : class, new()
        where TEntityEditRequestDto : class, new()
        where TEntityEditResponseDto : class, new()
    {
        public GenericNormalEditService(AbstractGenericRepository<TEntity, TContext> _repository, AbstractGenericMapHandler _mapper, AbstractGenericExceptionHandler _exceptionHandler, AbstractGenericLogWithSerilogHandler _logHandler) : base(_repository, _mapper, _exceptionHandler, _logHandler)
        {
        }
    }
}