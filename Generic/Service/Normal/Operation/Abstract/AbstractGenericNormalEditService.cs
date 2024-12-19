using AutoMapper;
using Generic.Base.Handler.Map;
using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.Repository.Abstract;
using Generic.Service.Normal.Operation.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Generic.Base.Handler.Map.GenericExceptionHandlerFactory;

namespace Generic.Service.Normal.Operation.Abstract
{
    public abstract class AbstractGenericNormalEditService<TContext, TEntity, TEntityEditRequestDto, TEntityEditResponseDto>
        : IGenericEditService<TEntity, TEntityEditRequestDto, TEntityEditResponseDto>
        where TContext : DbContext
        where TEntity : class, new()
        where TEntityEditRequestDto : class, new()
        where TEntityEditResponseDto : class, new()
    {
        private AbstractGenericRepository<TEntity, TContext> repository;
        private AbstractGenericMapHandler mapper;
        private AbstractGenericExceptionHandler exceptionHandler;
        private Serilog.ILogger logHandler;
        protected AbstractGenericNormalEditService(
            AbstractGenericRepository<TEntity, TContext> _repository,
            AbstractGenericMapHandler _mapper,
            AbstractGenericExceptionHandler _exceptionHandler,
            AbstractGenericLogWithSerilogHandler _logHandler
            )
        {
            repository = _repository;
            mapper = _mapper;
            exceptionHandler = _exceptionHandler;
            logHandler = _logHandler.CreateLogger();
        }
        public async Task<(bool, IEnumerable<TEntityEditResponseDto>)> EditGroup(IEnumerable<TEntityEditRequestDto> requestInput)
        {
            try
            {
                if (requestInput == null || !requestInput.Any())
                    return (true, null);

                bool resultIsSuccess = true;
                bool result = true;
                List<TEntityEditResponseDto> results = new List<TEntityEditResponseDto>();

                foreach (var req in requestInput)
                {
                    TEntity entity = new TEntity();
                    TEntityEditResponseDto responseTemp = new TEntityEditResponseDto();
                    try
                    {
                        entity = await mapper.Map<TEntityEditRequestDto, TEntity>(req);
                        await repository.SetEntityStateAsync(entity, EntityState.Detached);
                        result = await repository.UpdateAsync(entity);
                        if (!result)
                            throw exceptionHandler.PopException();
                        await repository.SaveAsync();
                        responseTemp = await mapper.Map<TEntity, TEntityEditResponseDto>(entity);
                    }
                    catch (Exception ex)
                    {
                        await repository.SetEntityStateAsync(entity, EntityState.Detached);
                        responseTemp = await mapper.Map<TEntity, TEntityEditResponseDto>(entity);
                        responseTemp = (TEntityEditResponseDto)await exceptionHandler.AssignExceptionInfoToObject(responseTemp, ex);
                      //  responseTemp.
                    }

                    results.Add(responseTemp);
                    if (!result)
                        resultIsSuccess = false;
                }

                await repository.CommitAsync();

                return (resultIsSuccess, results);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Helper.Helper.ServiceLog.FinallyAction(logHandler);
            }
        }

        public async Task<bool> EditRange(IEnumerable<TEntityEditRequestDto> requestInput)
        {
            List<TEntity> entityRequest = new List<TEntity>();
            try
            {
                if (requestInput == null || requestInput.Count() == 0)
                    return true;

                bool result = true;
               
                foreach (var req in requestInput)
                {
                    TEntity entity = new TEntity();
                    TEntityEditResponseDto responseTemp = new TEntityEditResponseDto();

                    entity = await mapper.Map<TEntityEditRequestDto, TEntity>(req);
                    
                    entityRequest.Add(entity);
                }
                result = await repository.UpdateRangeAsync(entityRequest);
                await repository.SaveAndCommitAsync();

                
                return result;
            }
            catch (Exception ex)
            {
                await repository.SetEntitiesStateAsync(entityRequest, EntityState.Detached);
                throw;
            }
            finally
            {
                Helper.Helper.ServiceLog.FinallyAction(logHandler);
            }
        }
    }
}
