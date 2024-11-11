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
    public abstract class AbstractGenericNormalPhysicalDeleteService<TContext, TEntity, TEntityDeleteRequestDto, TEntityDeleteResponseDto>
        :  IGenericPhisycalDeleteService<TEntity, TEntityDeleteRequestDto, TEntityDeleteResponseDto>
        where TContext : DbContext
        where TEntity : class, new()
        where TEntityDeleteRequestDto : class, new()
        where TEntityDeleteResponseDto : class, new()
    {
        private AbstractGenericRepository<TEntity, TContext> repository;
        private AbstractGenericMapHandler mapper;
        private AbstractGenericExceptionHandler exceptionHandler;
        private Serilog.ILogger logHandler;
        protected AbstractGenericNormalPhysicalDeleteService(
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

        public  async Task<(bool, IEnumerable<TEntityDeleteResponseDto>)> PhisycalDeleteGroup(IEnumerable<TEntityDeleteRequestDto> requestInput)
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

                        result = await repository.DeleteAsync(entity);
                        await repository.SaveAsync();
                        await repository.SetEntityStateAsync(entity, EntityState.Detached);
                    }
                    catch (Exception ex)
                    {

                        responseTemp = await mapper.Map<TEntity, TEntityDeleteResponseDto>(entity);


                        responseTemp = (TEntityDeleteResponseDto)await exceptionHandler.AssignExceptionInfoToObject(responseTemp, ex);
                        results.Add(responseTemp);
                    }

                    if (!result)
                        resultIsSuccess = false;

                    responseTemp = new TEntityDeleteResponseDto();
                    responseTemp = await mapper.Map<TEntity, TEntityDeleteResponseDto>(entity);
                    results.Add(responseTemp);

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

        public async Task<bool> PhisycalDeleteRange(IEnumerable<TEntityDeleteRequestDto> requestInput)
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
                    entityRequest.Add(entity);
                }
                result = await repository.DeleteRangeAsync(entityRequest);
                await repository.SaveAndCommitAsync();
                repository.SetEntityStateAsync(entityRequest, EntityState.Detached);
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
