using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.Repository.Abstract;
using Generic.Service.Normal.Operation.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Normal.Operation
{
    public class GenericNormalLogicalDeleteService<TContext, TEntity, TEntityDeleteRequestDto, TEntityDeleteResponseDto> : AbstractGenericNormalLogicalDeleteService<TContext, TEntity, TEntityDeleteRequestDto, TEntityDeleteResponseDto>
        where TContext : DbContext
        where TEntity : class, new()
        where TEntityDeleteRequestDto : class, new()
        where TEntityDeleteResponseDto : class, new()
    {
        public GenericNormalLogicalDeleteService(AbstractGenericRepository<TEntity, TContext> _repository, AbstractGenericMapHandler _mapper, AbstractGenericExceptionHandler _exceptionHandler, AbstractGenericLogWithSerilogHandler _logHandler) : base(_repository, _mapper, _exceptionHandler, _logHandler)
        {
        }
    }
}
