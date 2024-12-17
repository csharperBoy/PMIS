using Generic.Base.Handler.Map;
using Generic.Base.Handler.Map.Abstract;
using Generic.Service.Normal.Composition;
using Generic.Service.Normal.Operation.Abstract;
using GenericTest.Repository;
using Microsoft.EntityFrameworkCore;
using PMIS.DTO.User;
using PMIS.Models;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services
{
    public class UserService 
        : GenericNormalService<PmisContext, User, UserAddRequestDto, UserAddResponseDto, UserEditRequestDto, UserEditResponseDto, UserDeleteRequestDto, UserDeleteResponseDto, UserSearchResponseDto>
       , IUserService
    {
        public UserService(AbstractGenericMapHandler _mapper, AbstractGenericNormalAddService<PmisContext, User, UserAddRequestDto, UserAddResponseDto> _normalAddService, AbstractGenericNormalEditService<PmisContext, User, UserEditRequestDto, UserEditResponseDto> _normalEditService, AbstractGenericNormalLogicalDeleteService<PmisContext, User, UserDeleteRequestDto, UserDeleteResponseDto> _logicalDeleteService, AbstractGenericNormalPhysicalDeleteService<PmisContext, User, UserDeleteRequestDto, UserDeleteResponseDto> _physicalDeleteService, AbstractGenericNormalSearchService<PmisContext, User, UserSearchResponseDto> _searchService) : base(_mapper, _normalAddService, _normalEditService, _logicalDeleteService, _physicalDeleteService, _searchService)
        {
        }
        private AbstractGenericMapHandler mapper;
       
        public override async Task<bool> AddRange(IEnumerable<UserAddRequestDto> requestInput)
        {
            mapper = GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto);
            Repository<User,PmisContext> repository = new Repository<User, PmisContext> (new PmisContext());
            bool result = true;
            List<User> entityRequest = new List<User>();
            foreach (var req in requestInput)
            {
                User entity = new User();

                entity = await mapper.Map<UserAddRequestDto, User>(req);
                entityRequest.Add(entity);
            }           
               
            result = await repository.InsertRangeAsync(entityRequest);                       
            return result;
        }
    }
}
