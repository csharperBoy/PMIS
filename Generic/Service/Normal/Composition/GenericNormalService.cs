using AutoMapper;
using Generic.Base.Handler.Map;
using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Repository.Abstract;
using Generic.Service.Normal.Composition.Abstract;
using Generic.Service.Normal.Operation;
using Generic.Service.Normal.Operation.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generic.Service.DTO.Concrete;
using Generic.Service.DTO.Abstract;

namespace Generic.Service.Normal.Composition
{
    public class GenericNormalService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto, TEntityEditRequestDto, TEntityEditResponseDto, TEntityDeleteRequestDto, TEntityDeleteResponseDto, TEntitySearchResponseDto>
      : AbstractGenericNormalService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto, TEntityEditRequestDto, TEntityEditResponseDto, TEntityDeleteRequestDto, TEntityDeleteResponseDto, TEntitySearchResponseDto>
     where TContext : DbContext
     where TEntity : class, new()
     where TEntityAddRequestDto : class, new()
     where TEntityAddResponseDto : class, new()
     where TEntityEditRequestDto : class, new()
     where TEntityEditResponseDto : class, new()
     where TEntityDeleteRequestDto : class, new()
     where TEntityDeleteResponseDto : class, new()
     where TEntitySearchResponseDto : class, new()
    {
        AbstractGenericNormalAddService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto> normalAddService;
        AbstractGenericNormalEditService<TContext, TEntity, TEntityEditRequestDto, TEntityEditResponseDto> normalEditService;
        AbstractGenericNormalLogicalDeleteService<TContext, TEntity, TEntityDeleteRequestDto, TEntityDeleteResponseDto> logicalDeleteService;
        AbstractGenericNormalPhysicalDeleteService<TContext, TEntity, TEntityDeleteRequestDto, TEntityDeleteResponseDto> physicalDeleteService;
        AbstractGenericNormalSearchService<TContext, TEntity, TEntitySearchResponseDto> searchService;

        AbstractGenericMapHandler mapper;

        AbstractGenericRepository<TEntity, TContext> repository;
        AbstractGenericExceptionHandler exceptionHandler;

        public GenericNormalService(
            AbstractGenericMapHandler _mapper,

            AbstractGenericNormalAddService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto> _normalAddService,
            AbstractGenericNormalEditService<TContext, TEntity, TEntityEditRequestDto, TEntityEditResponseDto> _normalEditService,
            AbstractGenericNormalLogicalDeleteService<TContext, TEntity, TEntityDeleteRequestDto, TEntityDeleteResponseDto> _logicalDeleteService,
            AbstractGenericNormalPhysicalDeleteService<TContext, TEntity, TEntityDeleteRequestDto, TEntityDeleteResponseDto> _physicalDeleteService,
            AbstractGenericNormalSearchService<TContext, TEntity, TEntitySearchResponseDto> _searchService
        )
        {
            this.normalAddService = _normalAddService;
            this.normalEditService = _normalEditService;
            this.searchService = _searchService;
            this.physicalDeleteService = _physicalDeleteService;
            this.logicalDeleteService = _logicalDeleteService;
            this.mapper = _mapper;
           // this.mapper.MappingEvent += ExtraMap;
        }

        public virtual async Task<TDestination> ExtraMap<TSource, TDestination>(TSource source, TDestination destination)
            where TSource : class
            where TDestination : class, new()
        {
            return await Task.FromResult(destination);
        }

        //public async Task PerformMapping<TSource, TDestination>(TSource source, TDestination destination)
        //    where TSource : class
        //    where TDestination : class
        //{
        //    await mapper.ExtraMap<TSource, TDestination>(source, destination);
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

       
        public override async Task<(bool, IEnumerable<TEntitySearchResponseDto>)> Search(GenericSearchRequestDto requestInput)
        {
           return await searchService.Search(requestInput);
        }

        public override async Task<(bool, IEnumerable<TEntityDeleteResponseDto>)> LogicalDeleteGroup(IEnumerable<TEntityDeleteRequestDto> requestInput)
        {
            return await logicalDeleteService.LogicalDeleteGroup(requestInput);
        }

        public override async Task<(bool, IEnumerable<TEntityDeleteResponseDto>)> RecycleGroup(IEnumerable<TEntityDeleteRequestDto> requestInput)
        {
            return await logicalDeleteService.RecycleGroup(requestInput);
        }

        public override async Task<bool> LogicalDeleteRange(IEnumerable<TEntityDeleteRequestDto> requestInput)
        {
            return await logicalDeleteService.LogicalDeleteRange(requestInput);
        }

        public override async Task<bool> RecycleRange(IEnumerable<TEntityDeleteRequestDto> requestInput)
        {
            return await logicalDeleteService.RecycleRange(requestInput);
        }

        public override async Task<(bool, IEnumerable<TEntityDeleteResponseDto>)> PhisycalDeleteGroup(IEnumerable<TEntityDeleteRequestDto> requestInput)
        {
            return await physicalDeleteService.PhisycalDeleteGroup(requestInput);
        }

        public override async Task<bool> PhisycalDeleteRange(IEnumerable<TEntityDeleteRequestDto> requestInput)
        {
            return await physicalDeleteService.PhisycalDeleteRange(requestInput);
        }
    }
}
