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

namespace Generic.Service.Normal.Operation.Abstract
{
    public class AbstractGenericNormalSearchService<TContext, TEntity, TEntitySearchRequestDto, TEntitySearchResponseDto>
        : IGenericSearchService<TEntity, TEntitySearchRequestDto, TEntitySearchResponseDto>
        where TContext : DbContext
        where TEntity : class, new()
        where TEntitySearchRequestDto : class, new()
        where TEntitySearchResponseDto : class, new()
    {
        private AbstractGenericRepository<TEntity, TContext> repository;
        private AbstractGenericMapHandler mapper;
        private AbstractGenericExceptionHandler exceptionHandler;
        private Serilog.ILogger logHandler;
        protected AbstractGenericNormalSearchService(
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


        public async Task<(bool, IEnumerable<TEntitySearchResponseDto>)> Search(IEnumerable<TEntitySearchRequestDto> requestInput)
        {
            try
            {
                if (requestInput == null || requestInput.Count() == 0)
                    throw new Exception("لیست خالیست!!!");

                bool resultIsSuccess = true;
                bool result = true;
                List<TEntitySearchResponseDto> results = new List<TEntitySearchResponseDto>();
                foreach (var req in requestInput)
                {
                    TEntity entity = new TEntity();
                    TEntitySearchResponseDto responseTemp = new TEntitySearchResponseDto();
                    try
                    {
                        entity = await mapper.Map<TEntitySearchRequestDto, TEntity>(req);

                        result = await repository.GetPagingAsync(entity);
                        await repository.SaveAsync();
                        await repository.SetEntityStateAsync(entity, EntityState.Detached);
                    }
                    catch (Exception ex)
                    {

                        responseTemp = await mapper.Map<TEntity, TEntitySearchResponseDto>(entity);


                        responseTemp = (TEntitySearchResponseDto)await exceptionHandler.AssignExceptionInfoToObject(responseTemp, ex);
                        results.Add(responseTemp);
                    }

                    if (!result)
                        resultIsSuccess = false;

                    responseTemp = new TEntitySearchResponseDto();
                    responseTemp = await mapper.Map<TEntity, TEntitySearchResponseDto>(entity);
                    results.Add(responseTemp);

                }

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
    }
}
