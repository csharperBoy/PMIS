using Generic.Base.Handler.Map.Abstract;
using Generic.Service.Normal.Composition;
using Generic.Service.Normal.Operation.Abstract;
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
    }
}
