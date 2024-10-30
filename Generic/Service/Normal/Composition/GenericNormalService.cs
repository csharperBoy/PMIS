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

        
        public GenericNormalService(
            AbstractGenericRepository<TEntity, TContext> _repository,
            AbstractGenericMapHandler _mapper,
            AbstractGenericExceptionHandler _exceptionHandler
            )
        {
            normalAddService = new GenericNormalAddService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto>(_repository,_mapper,_exceptionHandler);
            normalEditService = new GenericNormalEditService<TContext, TEntity, TEntityEditRequestDto, TEntityEditResponseDto>();
        }

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
