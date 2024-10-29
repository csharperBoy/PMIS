using Generic.Repository.Contract;
using Generic.Repository;
using Generic.Service.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generic.Base.Handler.Map;
using Generic.Base.Handler.Map.Contract;
using Generic.Base.Handler.SystemException.Contract;
using Generic.Base.Handler.SystemException;

namespace Generic.Service.Abstract
{
    public abstract class GenericNormalService<TContext,TEntity, TEntityAddRequestDto, TEntityAddResponseDto, TEntityEditRequestDto, TEntityEditResponseDto> : 
        IGenericAddService<TEntity, TEntityAddRequestDto, TEntityAddResponseDto> ,
        IGenericEditService<TEntity, TEntityEditRequestDto, TEntityEditResponseDto>
        where TContext : DbContext
        where TEntity : class
        where TEntityAddRequestDto : class
        where TEntityAddResponseDto : class
        where TEntityEditRequestDto : class
        where TEntityEditResponseDto : class        
        
    {
        private TContext context;
        private IGenericRepository<TEntity> repository;
        private IGenericMapHandler mapper;
        private IGenericExceptionHandler exceptionHandler;
        private NormalAddService<TContext, TEntity, TEntityAddRequestDto, TEntityAddResponseDto> normalAddService;
        protected GenericNormalService()
        {
            repository = new SqlServerRepository<TEntity, TContext>(context);
            mapper = new AutoMapHandler();
            exceptionHandler = new MyExceptionHandler();
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
                        entity = await mapper.Map<TEntityAddRequestDto, TEntity>(req);

                        result = await repository.InsertAsync(entity);
                        await repository.SaveAsync();
                    }
                    catch (Exception ex)
                    {
                        TEntityAddResponseDto responseTemp = await mapper.Map<TEntity, TEntityAddResponseDto>(entity);


                        responseTemp = (TEntityAddResponseDto)await exceptionHandler.AssignExceptionInfoToObject(responseTemp, ex);
                        results.Add(responseTemp);
                    }

                    if (!result)
                        resultIsSuccess = false;


                    results.Add(await mapper.Map<TEntity, TEntityAddResponseDto>(entity));
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
        public Task<(bool, IEnumerable<TEntityEditResponseDto>)> EditGroup(IEnumerable<TEntityEditRequestDto> requestInput)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditRange(IEnumerable<TEntityEditRequestDto> requestInput)
        {
            throw new NotImplementedException();
        }

    }
}
