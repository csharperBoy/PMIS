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
using Generic.Service.DTO;

namespace Generic.Service.Normal.Composition.Abstract
;
public abstract class AbstractGenericNormalService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto, TEntityEditRequestDto, TEntityEditResponseDto, TEntityDeleteRequestDto, TEntityDeleteResponseDto, TEntitySearchResponseDto > :
    IGenericAddService<TEntity, TEntityAddRequestDto, TEntityAddResponseDto>,
    IGenericEditService<TEntity, TEntityEditRequestDto, TEntityEditResponseDto>,
    IGenericLogicalDeleteService<TEntity,  TEntityDeleteRequestDto, TEntityDeleteResponseDto>,
    IGenericPhisycalDeleteService<TEntity,  TEntityDeleteRequestDto, TEntityDeleteResponseDto>,
    IGenericSearchService<TEntity, TEntitySearchResponseDto>
    where TContext : DbContext
    where TEntity : class
    where TEntityAddRequestDto : class
    where TEntityAddResponseDto : class
    where TEntityEditRequestDto : class
    where TEntityEditResponseDto : class
    where TEntityDeleteRequestDto : class
    where TEntityDeleteResponseDto : class
    where TEntitySearchResponseDto : class
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


    public abstract Task<(bool, IEnumerable<TEntityDeleteResponseDto>)> LogicalDeleteGroup(IEnumerable<TEntityDeleteRequestDto> requestInput);



    public abstract Task<(bool, IEnumerable<TEntityDeleteResponseDto>)> RecycleGroup(IEnumerable<TEntityDeleteRequestDto> requestInput);



    public abstract Task<bool> LogicalDeleteRange(IEnumerable<TEntityDeleteRequestDto> requestInput);


    public abstract Task<bool> RecycleRange(IEnumerable<TEntityDeleteRequestDto> requestInput);


    public abstract Task<(bool, IEnumerable<TEntityDeleteResponseDto>)> PhisycalDeleteGroup(IEnumerable<TEntityDeleteRequestDto> requestInput);


    public abstract Task<bool> PhisycalDeleteRange(IEnumerable<TEntityDeleteRequestDto> requestInput);


    public abstract Task<(bool, IEnumerable<TEntitySearchResponseDto>)> Search(GenericSearchRequestDto requestInput);


}
