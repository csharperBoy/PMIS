﻿using AutoMapper;
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
                    return (true, null);

                bool resultIsSuccess = true;
                bool result = true;
                List<TEntityAddResponseDto> results = new List<TEntityAddResponseDto>();
                foreach (var req in requestInput)
                {
                    TEntity entity = new TEntity();
                    TEntityAddResponseDto responseTemp = new TEntityAddResponseDto();
                    try
                    {
                        entity = await mapper.Map<TEntityAddRequestDto, TEntity>(req);
                        await repository.SetEntityStateAsync(entity, EntityState.Detached);
                        result = await repository.InsertAsync(entity);
                        await repository.SaveAsync();
                        responseTemp = await mapper.Map<TEntity, TEntityAddResponseDto>(entity);
                    }
                    catch (Exception ex)
                    {
                        await repository.SetEntityStateAsync(entity, EntityState.Detached);
                        responseTemp = await mapper.Map<TEntity, TEntityAddResponseDto>(entity);
                        responseTemp = (TEntityAddResponseDto)await exceptionHandler.AssignExceptionInfoToObject(responseTemp, ex);
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

        public async Task<bool> AddRange(IEnumerable<TEntityAddRequestDto> requestInput)
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
                    TEntityAddResponseDto responseTemp = new TEntityAddResponseDto();

                    entity = await mapper.Map<TEntityAddRequestDto, TEntity>(req);
                    entityRequest.Add(entity);
                }
                result = await repository.InsertRangeAsync(entityRequest);
                await repository.SaveAndCommitAsync();

               

                return result;
            }
            catch (Exception ex)
            {
                await repository.SetEntitiesStateAsync(entityRequest, EntityState.Detached);
                await repository.RollbackAsync();
                throw;
            }
            finally
            {
                Helper.Helper.ServiceLog.FinallyAction(logHandler);
            }
        }
    }
}
