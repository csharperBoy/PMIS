using PMIS.Models;
using PMIS.Repository;
using PMIS.Repository.Contract;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services
{
    public abstract class GenericService<TEntity, TAddReqEntity, TAddResEntity, TEditReqEntity, TEditResEntity, TDeleteReqEntity, TDeleteResEntity, TSearchReqEntity, TSearchResEntity>
        : IGenericService<TEntity, TAddReqEntity, TAddResEntity, TEditReqEntity, TEditResEntity, TDeleteReqEntity, TDeleteResEntity, TSearchReqEntity, TSearchResEntity>
        where TEntity : class
        where TAddReqEntity : class
        where TAddResEntity : class
        where TEditReqEntity : class
        where TEditResEntity : class
        where TDeleteReqEntity : class
        where TDeleteResEntity : class
        where TSearchReqEntity : class
        where TSearchResEntity : class
    {
        private PmisContext context;
        private IGenericRepository<TEntity> repository;
        //private IUnitOfWork unitOfWork;
        public GenericService()
        {
            context = new PmisContext();
           // unitOfWork = new UnitOfWork();
            repository = new GenericRepository<TEntity, PmisContext>(context);
        }

        public virtual async Task<bool> AddRange(IEnumerable<TAddReqEntity> reqEntities)
        {

            try
            {
                if (reqEntities == null || reqEntities.Count() == 0)
                    throw new Exception("لیست خالیست!!!");

                List<TEntity> entities = new List<TEntity>();
                foreach (var reqEntity in reqEntities)
                {
                    entities.Add(mapAddReqToEntity(reqEntity));
                }
                 
                bool result = await repository.InsertRangeAsync(entities);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual async Task<(bool, IEnumerable<TAddResEntity>)> AddGroup(IEnumerable<TAddReqEntity> reqEntities)
        {
            try
            {
                if (reqEntities == null || reqEntities.Count() == 0)
                    throw new Exception("لیست خالیست!!!");

                bool resultIsSuccess = true;
                bool result = true;
                List<TAddResEntity> results = new List<TAddResEntity>();               
                foreach (var req in reqEntities)
                {
                    TEntity entity = null ;
                    try
                    {
                        entity = mapAddReqToEntity(req);

                        result = await repository.InsertAsync(entity);
                        await repository.SaveAsync();
                    }
                    catch (Exception ex)
                    {
                        results.Add(mapEntityToAddRes(entity , ex.Message));
                    }
                    
                    if (!result)                    
                        resultIsSuccess = false;
                    
                    
                    results.Add(mapEntityToAddRes(entity));
                }
                await repository.CommitAsync();
                return (resultIsSuccess, results);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<(bool, IEnumerable<TDeleteResEntity>)> DeleteGroup(IEnumerable<TDeleteReqEntity> reqEntities)
        {
            try
            {
                if (reqEntities == null || reqEntities.Count() == 0)
                    throw new Exception("لیست خالیست!!!");

                bool resultIsSuccess = true;
                List<TDeleteResEntity> results = new List<TDeleteResEntity>();
                
                foreach (var req in reqEntities)
                {
                    TEntity entity = mapDeleteReqToEntity(req);
                    bool result = repository.Delete(entity);
                    if (!result)
                        resultIsSuccess = false;
                    results.Add(mapEntityToDeleteRes(entity));
                }

                return (resultIsSuccess, await Task.FromResult(results));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual async Task<bool> EditRange(IEnumerable<TEditReqEntity> reqEntities)
        {
            try
            {
                if (reqEntities == null || reqEntities.Count() == 0)
                    throw new Exception("لیست خالیست!!!");
                List<TEntity> entities = new List<TEntity>();
                foreach (var reqEntity in reqEntities)
                {
                    entities.Add(mapEditReqToEntity(reqEntity));
                }
               
                bool result = repository.UpdateRange(entities);

                return await Task.FromResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public virtual async Task<(bool, IEnumerable<TEditResEntity>)> EditGroup(IEnumerable<TEditReqEntity> reqEntities)
        {
            try
            {
                if (reqEntities == null || reqEntities.Count() == 0)
                    throw new Exception("لیست خالیست!!!");

                bool resultIsSuccess = true;
                List<TEditResEntity> results = new List<TEditResEntity>();
                foreach (var req in reqEntities)
                {
                    TEntity entity = mapEditReqToEntity(req);
                    bool result = repository.Update(entity);
                    if (!result)
                        resultIsSuccess = false;
                    results.Add(mapEntityToEditRes(entity));
                }

                return (resultIsSuccess, await Task.FromResult(results));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual async Task<IEnumerable<TSearchResEntity>> Search(IEnumerable<TSearchReqEntity> request)
        {
            try
            {
                List<TSearchResEntity> result = new List<TSearchResEntity>();
                foreach (var item in request)
                {
                    IEnumerable<TSearchResEntity> response = await Search(item);
                    //resultDetails = resultDetails.Concat(response.Where(a => !resultDetails.Any(b => a.Id == b.Id))).ToList();
                    result = result.Concat(response).ToList();
                }
                //return new ObservableCollection<TSearchResEntity>(resultDetails);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected virtual async Task<IEnumerable<TSearchResEntity>> Search(TSearchReqEntity request)
        {
            try
            {
                bool IsSuccess;
                Expression<Func<TEntity, bool>> filterExpression = null;
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null;
                string includeProperties = "";
                int pageNumber = 0;
                int recordCount = 0;
                (IsSuccess, filterExpression, orderBy, includeProperties, pageNumber, recordCount) = await DecodeSearchRequest(request);
                (IEnumerable<TEntity> entities, int count) = await repository.GetPaging(filterExpression, orderBy, includeProperties, pageNumber, recordCount);
                IEnumerable<TSearchResEntity> result = mapEntitiesToSearchResult(entities, count);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected virtual IEnumerable<TSearchResEntity> mapEntitiesToSearchResult(IEnumerable<TEntity> entities, int count)
        {
            throw new NotImplementedException();
        }

        protected virtual async Task<(bool, Expression<Func<TEntity, bool>>, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>, string, int, int)> DecodeSearchRequest(TSearchReqEntity filters)
        {
            return (true, null, null, "", 0, 0);
        }
        protected virtual TEntity mapAddReqToEntity(TAddReqEntity reqEntities)
        {
            throw new NotImplementedException();
        }
        protected virtual TDeleteResEntity mapEntityToDeleteRes(TEntity entity)
        {
            throw new NotImplementedException();
        }
        protected virtual TEntity mapDeleteReqToEntity(TDeleteReqEntity reqEntities)
        {
            throw new NotImplementedException();
        }

        protected virtual TAddResEntity mapEntityToAddRes(TEntity entity , string? errorMessage = null)
        {
            throw new NotImplementedException();
        }
        protected virtual TEntity mapEditReqToEntity(TEditReqEntity reqEntities)
        {
            throw new NotImplementedException();
        }

        protected virtual TEditResEntity mapEntityToEditRes(TEntity entity)
        {
            throw new NotImplementedException();
        }


    }
}
