using Generic.Base.Handler.Map.Abstract;
using Generic.Service.Normal.Composition;
using Generic.Service.Normal.Operation.Abstract;
using PMIS.DTO.ClaimUserOnIndicator;
using PMIS.Models;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services
{
    public class ClaimUserOnIndicatorService
        : GenericNormalService<PmisContext, ClaimUserOnIndicator, ClaimUserOnIndicatorAddRequestDto, ClaimUserOnIndicatorAddResponseDto, ClaimUserOnIndicatorEditRequestDto, ClaimUserOnIndicatorEditResponseDto, ClaimUserOnIndicatorDeleteRequestDto, ClaimUserOnIndicatorDeleteResponseDto, ClaimUserOnIndicatorSearchResponseDto>
       , IClaimUserOnIndicatorService
    {
        public ClaimUserOnIndicatorService(AbstractGenericMapHandler _mapper, AbstractGenericNormalAddService<PmisContext, ClaimUserOnIndicator, ClaimUserOnIndicatorAddRequestDto, ClaimUserOnIndicatorAddResponseDto> _normalAddService, AbstractGenericNormalEditService<PmisContext, ClaimUserOnIndicator, ClaimUserOnIndicatorEditRequestDto, ClaimUserOnIndicatorEditResponseDto> _normalEditService, AbstractGenericNormalLogicalDeleteService<PmisContext, ClaimUserOnIndicator, ClaimUserOnIndicatorDeleteRequestDto, ClaimUserOnIndicatorDeleteResponseDto> _logicalDeleteService, AbstractGenericNormalPhysicalDeleteService<PmisContext, ClaimUserOnIndicator, ClaimUserOnIndicatorDeleteRequestDto, ClaimUserOnIndicatorDeleteResponseDto> _physicalDeleteService, AbstractGenericNormalSearchService<PmisContext, ClaimUserOnIndicator, ClaimUserOnIndicatorSearchResponseDto> _searchService) : base(_mapper, _normalAddService, _normalEditService, _logicalDeleteService, _physicalDeleteService, _searchService)
        {
        }
    }
}
