using AutoMapper;

using Generic.Base.Handler.Map;
using Generic.Repository;
using Generic.Repository.Contract;
using Generic.Service.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.Abstract
{
    public abstract class GenericNormalAddService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto>
        : AutoMapHandler
        , IGenericAddService<TEntity, TEntityAddRequestDto, TEntityAddResponseDto>
        where TContext : DbContext
        where TEntity : class
        where TEntityAddRequestDto : class
        where TEntityAddResponseDto : class
    {
        private TContext context;
        private IGenericRepository<TEntity> repository;
        protected GenericNormalAddService()
        {
            repository = new SqlServerRepository<TEntity, TContext>(context);
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
                    TEntity entity = null;
                    try
                    {
                        entity = await Map<TEntityAddRequestDto, TEntity>(req);

                        result = await repository.InsertAsync(entity);
                        await repository.SaveAsync();
                    }
                    catch (Exception ex)
                    {
                        TEntityAddResponseDto responseTemp = await Map<TEntity, TEntityAddResponseDto>(entity);


                        responseTemp = (TEntityAddResponseDto)await AssignExceptionInfoToObject(responseTemp, ex);
                        results.Add(responseTemp);
                    }

                    if (!result)
                        resultIsSuccess = false;


                    results.Add(await Map<TEntity, TEntityAddResponseDto>(entity));
                }
                await repository.CommitAsync();
                return (resultIsSuccess, results);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected virtual async Task<object> AssignExceptionInfoToObject(object responseTemp, Exception ex)
        {
            var errorMessageProperty = typeof(object).GetProperty("ErrorMessage");
            if (errorMessageProperty != null)
            {
                errorMessageProperty.SetValue(responseTemp, ex.Message);
            }
            return responseTemp;
        }

        public Task<bool> AddRange(IEnumerable<TEntityAddRequestDto> requestInput)
        {
            throw new NotImplementedException();
        }



    }
}
