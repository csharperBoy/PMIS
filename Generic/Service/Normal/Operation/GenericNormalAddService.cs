using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.SystemException.Abstract;
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
    internal class GenericNormalAddService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto> : AbstractGenericNormalAddService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto>
        where TContext : DbContext
        where TEntity : class, new()
        where TEntityAddRequestDto : class, new()
        where TEntityAddResponseDto : class, new()
    {
        public GenericNormalAddService(AbstractGenericRepository<TEntity, TContext> _repository, AbstractGenericMapHandler _mapper, AbstractGenericExceptionHandler _exceptionHandler) : base(_repository, _mapper, _exceptionHandler)
        {
        }
    }
}
