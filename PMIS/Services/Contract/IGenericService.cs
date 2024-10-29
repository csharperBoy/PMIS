using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services.Contract
{
    public interface IGenericService<TEntity,TAddReqEntity, TAddResEntity, TEditReqEntity, TEditResEntity, TDeleteReqEntity, TDeleteResEntity ,TSearchReqEntity , TSearchResEntity>
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
        Task<bool> AddRange(IEnumerable<TAddReqEntity> reqEntities);       
        Task<(bool, IEnumerable<TAddResEntity>)> AddGroup(IEnumerable<TAddReqEntity> reqEntities);
        Task<(bool, IEnumerable<TEditResEntity>)> EditGroup(IEnumerable<TEditReqEntity> reqEntities);
        Task<bool> EditRange(IEnumerable<TEditReqEntity> reqEntities);
        Task<(bool, IEnumerable<TDeleteResEntity>)> DeleteGroup(IEnumerable<TDeleteReqEntity> reqEntities);
        Task<IEnumerable<TSearchResEntity>> Search(IEnumerable<TSearchReqEntity> request);
    }
}
