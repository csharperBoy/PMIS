using AutoMapper;
using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Repository.Abstract;
using Generic.Service.Normal.Composition.Abstract;
using Generic.Service.Normal.Operation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Normal.Composition
{
    public class GenericNormalService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto, TEntityEditRequestDto, TEntityEditResponseDto>
         : AbstractGenericNormalService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto, TEntityEditRequestDto, TEntityEditResponseDto>
        where TContext : DbContext
        where TEntity : class
        where TEntityAddRequestDto : class
        where TEntityAddResponseDto : class
        where TEntityEditRequestDto : class
        where TEntityEditResponseDto : class
    {
        GenericNormalAddService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto> normalAddService;
        GenericNormalEditService<TContext, TEntity, TEntityEditRequestDto, TEntityEditResponseDto> normalEditService;

        AbstractGenericMapHandler mapper;
        public GenericNormalService(
            AbstractGenericRepository<TEntity, TContext> _repository,
            AbstractGenericMapHandler _mapper,
            AbstractGenericExceptionHandler _exceptionHandler
            )
        {
            normalAddService = new GenericNormalAddService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto>(_repository,_mapper,_exceptionHandler);
            normalEditService = new GenericNormalEditService<TContext, TEntity, TEntityEditRequestDto, TEntityEditResponseDto>();
            mapper = _mapper;
            mapper.MappingEvent += MyMapping;
        }
        public virtual async Task MyMapping<TSource, TDestination>(TSource source, TDestination destination)
        where TSource : class
        where TDestination : class
        {

            // می‌توانید منطق اضافی خود را در اینجا اضافه کنید.  
            await Task.CompletedTask; // برای همگام سازی  
        }

        public async Task<TDestination> PerformMapping<TSource, TDestination>(TSource source, TDestination destination)
            where TSource : class
            where TDestination : class
        {
            return await mapper.Mapping(source, destination);
        }

        //public virtual async Task<TDestination> Map<TSource, TDestination>(TSource source)
        //    where TSource : class
        //    where TDestination : class
        //{

        //    TDestination destination =await mapper.Map<TSource,TDestination>(source);
        //    destination = await ExtraMap(source, destination);
        //    return await Task.FromResult(destination);
        //}
        //public virtual async Task<TDestination> ExtraMap<TSource, TDestination>(TSource source, TDestination destination)
        //    where TSource : class
        //    where TDestination : class
        //{
        //    try
        //    {                
        //        return await Task.FromResult(destination);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public override async Task<(bool, IEnumerable<TEntityAddResponseDto>)> AddGroup(IEnumerable<TEntityAddRequestDto> requestInput)
        {
            return await normalAddService.AddGroup(requestInput);
        }

        public override async Task<bool> AddRange(IEnumerable<TEntityAddRequestDto> requestInput)
        {
            return await normalAddService.AddRange(requestInput);
        }

        public override async Task<(bool, IEnumerable<TEntityEditResponseDto>)> EditGroup(IEnumerable<TEntityEditRequestDto> requestInput)
        {
            return await normalEditService.EditGroup(requestInput);
        }

        public override async Task<bool> EditRange(IEnumerable<TEntityEditRequestDto> requestInput)
        {
            return await normalEditService.EditRange(requestInput);
        }
    }
}
