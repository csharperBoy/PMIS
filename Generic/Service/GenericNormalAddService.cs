using AutoMapper;
using Generic.Base;
using Generic.Base.Contract;
using Generic.Repository;
using Generic.Repository.Contract;
using Generic.Service.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service
{
    public abstract class GenericNormalAddService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto> 
        : GenericMapper
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
            repository = new GenericSqlServerRepository<TEntity,TContext>(context);
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
                        //entity = await mapperRequestToEntity.Map(req);

                        result = await repository.InsertAsync(entity);
                        await repository.SaveAsync();
                    }
                    catch (Exception ex)
                    {
                       // results.Add(mapperEntityToResponse.Map(entity, ex.Message));
                    }

                    if (!result)
                        resultIsSuccess = false;


                    //results.Add(mapperEntityToResponse.Map(entity));
                }
                await repository.CommitAsync();
                return (resultIsSuccess, results);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<bool> AddRange(IEnumerable<TEntityAddRequestDto> requestInput)
        {
            throw new NotImplementedException();
        }
        

        
    }
}
