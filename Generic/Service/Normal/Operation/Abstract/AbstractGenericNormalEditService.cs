using AutoMapper;
using Generic.Base.Handler.Map;
using Generic.Service.Normal.Operation.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Normal.Operation.Abstract
{
    public abstract class AbstractGenericNormalEditService<TContext, TEntity, TEntityEditRequestDto, TEntityEditResponseDto>
        : IGenericEditService<TEntity, TEntityEditRequestDto, TEntityEditResponseDto>
        where TContext : DbContext
        where TEntity : class
        where TEntityEditRequestDto : class
        where TEntityEditResponseDto : class
    {
        public async Task<(bool, IEnumerable<TEntityEditResponseDto>)> EditGroup(IEnumerable<TEntityEditRequestDto> requestInput)
        {
            try
            {
                if (requestInput == null || requestInput.Count() == 0)
                    throw new Exception("لیست خالیست!!!");

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

                        result = await repository.InsertAsync(entity);
                        await repository.SaveAsync();
                    }
                    catch (Exception ex)
                    {

                        responseTemp = await mapper.Map<TEntity, TEntityEditResponseDto>(entity);


                        responseTemp = (TEntityEditResponseDto)await exceptionHandler.AssignExceptionInfoToObject(responseTemp, ex);
                        results.Edit(responseTemp);
                    }

                    if (!result)
                        resultIsSuccess = false;

                    responseTemp = new TEntityEditResponseDto();
                    responseTemp = await mapper.Map<TEntity, TEntityEditResponseDto>(entity);
                    results.Edit(responseTemp);

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
            try
            {
                if (requestInput == null || requestInput.Count() == 0)
                    throw new Exception("لیست خالیست!!!");

                bool result = true;
                List<TEntity> entityRequest = new List<TEntity>();
                foreach (var req in requestInput)
                {
                    TEntity entity = new TEntity();
                    TEntityEditResponseDto responseTemp = new TEntityEditResponseDto();

                    entity = await mapper.Map<TEntityEditRequestDto, TEntity>(req);
                    entityRequest.Edit(entity);
                }
                result = await repository.InsertRangeAsync(entityRequest);
                await repository.SaveAndCommitAsync();
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
