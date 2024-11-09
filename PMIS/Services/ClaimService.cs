using Generic.Base.Handler.Map.Abstract;
using Generic.Service.Normal.Composition;
using Generic.Service.Normal.Operation.Abstract;
using PMIS.DTO.Claim;
using PMIS.Models;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services
{
    public class ClaimService
        : GenericNormalService<PmisContext, Claim, ClaimAddRequestDto, ClaimAddResponseDto, ClaimEditRequestDto, ClaimEditResponseDto, ClaimDeleteRequestDto, ClaimDeleteResponseDto, ClaimSearchResponseDto>
       , IClaimService
    {
        public ClaimService(AbstractGenericMapHandler _mapper, AbstractGenericNormalAddService<PmisContext, Claim, ClaimAddRequestDto, ClaimAddResponseDto> _normalAddService, AbstractGenericNormalEditService<PmisContext, Claim, ClaimEditRequestDto, ClaimEditResponseDto> _normalEditService, AbstractGenericNormalLogicalDeleteService<PmisContext, Claim, ClaimDeleteRequestDto, ClaimDeleteResponseDto> _logicalDeleteService, AbstractGenericNormalPhysicalDeleteService<PmisContext, Claim, ClaimDeleteRequestDto, ClaimDeleteResponseDto> _physicalDeleteService, AbstractGenericNormalSearchService<PmisContext, Claim, ClaimSearchResponseDto> _searchService) : base(_mapper, _normalAddService, _normalEditService, _logicalDeleteService, _physicalDeleteService, _searchService)
        {
        }
    }
}
