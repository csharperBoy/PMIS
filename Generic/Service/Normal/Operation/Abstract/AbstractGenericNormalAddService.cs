using AutoMapper;
using Generic.Base.General;
using Generic.Base.Handler.Map;
using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.Repository;
using Generic.Repository.Abstract;
using Generic.Repository.Contract;
using Generic.Service.Normal.Operation.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Generic.Base.Handler.Map.Concrete.GenericAutoMapHandler;



namespace Generic.Service.Normal.Operation.Abstract
{
    public abstract class AbstractGenericNormalAddService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto>
        : IGenericAddService<TEntity, TEntityAddRequestDto, TEntityAddResponseDto>
        where TContext : DbContext
        where TEntity : class, new()
        where TEntityAddRequestDto : class, new()
        where TEntityAddResponseDto : class, new()
    {
        private AbstractGenericRepository<TEntity, TContext> repository;
        private AbstractGenericMapHandler mapper;
        private AbstractGenericExceptionHandler exceptionHandler;
        private Serilog.ILogger logHandler;
        protected AbstractGenericNormalAddService(
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




        public async Task<(bool, IEnumerable<TEntityAddResponseDto>)> AddGroup(IEnumerable<TEntityAddRequestDto> requestInput)
        {
            try
            {
                if (requestInput == null || requestInput.Count() == 0)
                    throw new Exception("لیست خالیست!!!");

                bool resultIsSuccess = true;
                bool result = true;
                List<TEntityAddResponseDto> results = new List<TEntityAddResponseDto>();
                foreach (var req in requestInput)
                {
                    TEntity entity = new TEntity();
                    TEntityAddResponseDto responseTemp = new TEntityAddResponseDto();
                    try
                    {
                        entity = await mapper.Map<TEntityAddRequestDto, TEntity>(req, opts =>
                        {
                            opts.BeforeMap(async (src, dest) =>
                            {
                                var mapMethod = typeof(TEntityAddRequestDto).GetMethod("BeforeMap", BindingFlags.Static | BindingFlags.Public);
                                if (mapMethod != null)
                                {
                                    var genericMethod = mapMethod.MakeGenericMethod(typeof(TEntityAddRequestDto), typeof(TEntity));

                                    var task = (Task)genericMethod.Invoke(null, new object[] { src, dest });
                                    await task.ConfigureAwait(false);

                                    var resultProperty = task.GetType().GetProperty("Result");
                                    if (resultProperty != null)
                                    {
                                        var result = (TEntity)resultProperty.GetValue(task);

                                    }
                                }
                            });

                            opts.AfterMap(async (src, dest) =>
                            {
                                var mapMethod = typeof(TEntityAddRequestDto).GetMethod("AfterMap", BindingFlags.Static | BindingFlags.Public);
                                if (mapMethod != null)
                                {
                                    var genericMethod = mapMethod.MakeGenericMethod(typeof(TEntityAddRequestDto), typeof(TEntity));

                                    var task = (Task)genericMethod.Invoke(null, new object[] { src, dest });
                                    await task.ConfigureAwait(false);

                                    var resultProperty = task.GetType().GetProperty("Result");
                                    if (resultProperty != null)
                                    {
                                        var result = (TEntity)resultProperty.GetValue(task);

                                    }
                                }
                            });
                        });

                        result = await repository.InsertAsync(entity);
                        await repository.SaveAsync();
                        repository.SetEntityStateAsync(entity,EntityState.Detached);
                    }
                    catch (Exception ex)
                    {

                        responseTemp = await mapper.Map<TEntity, TEntityAddResponseDto>(entity);


                        responseTemp = (TEntityAddResponseDto)await exceptionHandler.AssignExceptionInfoToObject(responseTemp, ex);
                        results.Add(responseTemp);
                    }

                    if (!result)
                        resultIsSuccess = false;

                    responseTemp = new TEntityAddResponseDto();
                    responseTemp = await mapper.Map<TEntity, TEntityAddResponseDto>(entity);
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

        

        public async Task<bool> AddRange(IEnumerable<TEntityAddRequestDto> requestInput)
        {
            try
            {
                if (requestInput == null || requestInput.Count() == 0)
                    throw new Exception("لیست خالیست!!!");

                bool result = true;
                List<TEntity> entityRequest = new List<TEntity>();
                foreach (var req in requestInput)
                {
                    TEntity entity = new TEntity();
                    TEntityAddResponseDto responseTemp = new TEntityAddResponseDto();

                    entity = await mapper.Map<TEntityAddRequestDto, TEntity>(req);
                    entityRequest.Add(entity);
                }
                result = await repository.InsertRangeAsync(entityRequest);
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
