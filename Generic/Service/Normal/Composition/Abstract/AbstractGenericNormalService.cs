using Generic.Repository.Contract;
using Generic.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generic.Base.Handler.Map;
using Generic.Base.Handler.Map.Contract;
using Generic.Base.Handler.SystemException.Contract;
using Generic.Base.Handler.SystemException;
using Generic.Service.Normal.Operation.Contract;

namespace Generic.Service.Normal.Composition.Abstract
{
    public abstract class AbstractGenericNormalService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto, TEntityEditRequestDto, TEntityEditResponseDto> :
        IGenericAddService<TEntity, TEntityAddRequestDto, TEntityAddResponseDto>,
        IGenericEditService<TEntity, TEntityEditRequestDto, TEntityEditResponseDto>
        where TContext : DbContext
        where TEntity : class
        where TEntityAddRequestDto : class
        where TEntityAddResponseDto : class
        where TEntityEditRequestDto : class
        where TEntityEditResponseDto : class

    {
        //private TContext context;
        //private IGenericRepository<TEntity> repository;
        //private IGenericMapHandler mapper;
        //private IGenericExceptionHandler exceptionHandler;
        protected AbstractGenericNormalService()
        {
            //repository = new GenericSqlServerRepository<TEntity, TContext>(context);
            //mapper = new GenericAutoMapHandler();
            //exceptionHandler = new GenericMyExceptionHandler();
        }

        public abstract Task<(bool, IEnumerable<TEntityAddResponseDto>)> AddGroup(IEnumerable<TEntityAddRequestDto> requestInput);

        public abstract Task<bool> AddRange(IEnumerable<TEntityAddRequestDto> requestInput);

        public abstract Task<(bool, IEnumerable<TEntityEditResponseDto>)> EditGroup(IEnumerable<TEntityEditRequestDto> requestInput);


        public abstract Task<bool> EditRange(IEnumerable<TEntityEditRequestDto> requestInput);
        

    }
}
