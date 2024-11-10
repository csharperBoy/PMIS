using Generic.Base.Handler.Map.Abstract;
using Generic.Service.Normal.Composition;
using Generic.Service.Normal.Operation.Abstract;
using PMIS.DTO.ClaimOnSystem;
using PMIS.Models;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services
{
    public class ClaimOnSystemService : GenericNormalService<PmisContext, ClaimOnSystem, ClaimOnSystemAddRequestDto, ClaimOnSystemAddResponseDto, ClaimOnSystemEditRequestDto, ClaimOnSystemEditResponseDto, ClaimOnSystemDeleteRequestDto, ClaimOnSystemDeleteResponseDto, ClaimOnSystemSearchResponseDto>
       , IClaimOnSystemService
    {
        public ClaimOnSystemService(AbstractGenericMapHandler _mapper, AbstractGenericNormalAddService<PmisContext, ClaimOnSystem, ClaimOnSystemAddRequestDto, ClaimOnSystemAddResponseDto> _normalAddService, AbstractGenericNormalEditService<PmisContext, ClaimOnSystem, ClaimOnSystemEditRequestDto, ClaimOnSystemEditResponseDto> _normalEditService, AbstractGenericNormalLogicalDeleteService<PmisContext, ClaimOnSystem, ClaimOnSystemDeleteRequestDto, ClaimOnSystemDeleteResponseDto> _logicalDeleteService, AbstractGenericNormalPhysicalDeleteService<PmisContext, ClaimOnSystem, ClaimOnSystemDeleteRequestDto, ClaimOnSystemDeleteResponseDto> _physicalDeleteService, AbstractGenericNormalSearchService<PmisContext, ClaimOnSystem, ClaimOnSystemSearchResponseDto> _searchService) : base(_mapper, _normalAddService, _normalEditService, _logicalDeleteService, _physicalDeleteService, _searchService)
        {
        }
    }
}
