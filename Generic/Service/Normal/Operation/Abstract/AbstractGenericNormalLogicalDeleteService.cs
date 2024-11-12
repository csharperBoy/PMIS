using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.Repository.Abstract;
using Generic.Service.Normal.Operation.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Normal.Operation.Abstract
{
    public abstract class AbstractGenericNormalLogicalDeleteService<TContext, TEntity, TEntityDeleteRequestDto, TEntityDeleteResponseDto>
        : IGenericLogicalDeleteService<TEntity, TEntityDeleteRequestDto, TEntityDeleteResponseDto>
        where TContext : DbContext
        where TEntity : class, new()
        where TEntityDeleteRequestDto : class, new()
        where TEntityDeleteResponseDto : class, new()
    {
        private AbstractGenericRepository<TEntity, TContext> repository;
        private AbstractGenericMapHandler mapper;
        private AbstractGenericExceptionHandler exceptionHandler;
        private Serilog.ILogger logHandler;
        protected AbstractGenericNormalLogicalDeleteService(
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

        public async Task<(bool, IEnumerable<TEntityDeleteResponseDto>)> LogicalDeleteGroup(IEnumerable<TEntityDeleteRequestDto> requestInput)
        {
            try
            {
                if (requestInput == null || requestInput.Count() == 0)
                     return (true, null);

                bool resultIsSuccess = true;
                bool result = true;
                List<TEntityDeleteResponseDto> results = new List<TEntityDeleteResponseDto>();
                foreach (var req in requestInput)
                {
                    TEntity entity = new TEntity();
                    TEntityDeleteResponseDto responseTemp = new TEntityDeleteResponseDto();
                    try
                    {
                        entity = await mapper.Map<TEntityDeleteRequestDto, TEntity>(req);

                        var entityFields = entity.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        var fieldName = entityFields.FirstOrDefault(f => f.Name == "FlgLogicalDelete");

                        if (fieldName != null)
                        {
                            fieldName.SetValue(entity, true);
                        }

                        await repository.SetEntityStateAsync(entity, EntityState.Detached);
                        result = await repository.UpdateAsync(entity);
                        await repository.SaveAsync();
                        responseTemp = await mapper.Map<TEntity, TEntityDeleteResponseDto>(entity);
                    }
                    catch (Exception ex)
                    {
                        responseTemp = await mapper.Map<TEntity, TEntityDeleteResponseDto>(entity);
                        responseTemp = (TEntityDeleteResponseDto)await exceptionHandler.AssignExceptionInfoToObject(responseTemp, ex);
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

        public async Task<bool> LogicalDeleteRange(IEnumerable<TEntityDeleteRequestDto> requestInput)
        {
            try
            {
                if (requestInput == null || requestInput.Count() == 0)
                      return true;

                bool result = true;
                List<TEntity> entityRequest = new List<TEntity>();
                foreach (var req in requestInput)
                {
                    TEntity entity = new TEntity();
                    TEntityDeleteResponseDto responseTemp = new TEntityDeleteResponseDto();

                    entity = await mapper.Map<TEntityDeleteRequestDto, TEntity>(req);

                    var entityFields = entity.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var fieldName = entityFields.FirstOrDefault(f => f.Name.Contains( "FlgLogicalDelete"));

                    if (fieldName != null)
                    {
                        fieldName.SetValue(entity, true);
                    }

                    entityRequest.Add(entity);
                }
                result = await repository.UpdateRangeAsync(entityRequest);
                await repository.SaveAndCommitAsync();
               // await repository.SetEntityStateAsync(entityRequest, EntityState.Detached);
                return result;
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

        public async Task<(bool, IEnumerable<TEntityDeleteResponseDto>)> RecycleGroup(IEnumerable<TEntityDeleteRequestDto> requestInput)
        {
            try
            {
                if (requestInput == null || requestInput.Count() == 0)
                     return (true, null);

                bool resultIsSuccess = true;
                bool result = true;
                List<TEntityDeleteResponseDto> results = new List<TEntityDeleteResponseDto>();
                foreach (var req in requestInput)
                {
                    TEntity entity = new TEntity();
                    TEntityDeleteResponseDto responseTemp = new TEntityDeleteResponseDto();
                    try
                    {
                        entity = await mapper.Map<TEntityDeleteRequestDto, TEntity>(req);

                        var entityFields = entity.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        var fieldName = entityFields.FirstOrDefault(f => f.Name == "FlgLogicalDelete");

                        if (fieldName != null)
                        {
                            fieldName.SetValue(entity, false);
                        }
                        await repository.SetEntityStateAsync(entity, EntityState.Detached);
                        result = await repository.UpdateAsync(entity);
                        await repository.SaveAsync();
                        responseTemp = await mapper.Map<TEntity, TEntityDeleteResponseDto>(entity);
                    }
                    catch (Exception ex)
                    {
                        responseTemp = await mapper.Map<TEntity, TEntityDeleteResponseDto>(entity);
                        responseTemp = (TEntityDeleteResponseDto)await exceptionHandler.AssignExceptionInfoToObject(responseTemp, ex);
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

        public async Task<bool> RecycleRange(IEnumerable<TEntityDeleteRequestDto> requestInput)
        {
            try
            {
                if (requestInput == null || requestInput.Count() == 0)
                      return true;

                bool result = true;
                List<TEntity> entityRequest = new List<TEntity>();
                foreach (var req in requestInput)
                {
                    TEntity entity = new TEntity();
                    TEntityDeleteResponseDto responseTemp = new TEntityDeleteResponseDto();

                    entity = await mapper.Map<TEntityDeleteRequestDto, TEntity>(req);

                    var entityFields = entity.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var fieldName = entityFields.FirstOrDefault(f => f.Name == "FlgLogicalDelete");

                    if (fieldName != null)
                    {
                        fieldName.SetValue(entity, false);
                    }

                    entityRequest.Add(entity);
                }
                result = await repository.UpdateRangeAsync(entityRequest);
                await repository.SaveAndCommitAsync();
               // await repository.SetEntityStateAsync(entityRequest, EntityState.Detached);
                return result;
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
    }
}
