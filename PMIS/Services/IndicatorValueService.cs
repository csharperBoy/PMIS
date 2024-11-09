using Generic.Base.Handler.Map.Abstract;
using Generic.Service.Normal.Composition;
using Generic.Service.Normal.Operation.Abstract;
using PMIS.DTO.IndicatorValue;
using PMIS.Models;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services
{
    public class IndicatorValueService : GenericNormalService<PmisContext, IndicatorValue, IndicatorValueAddRequestDto, IndicatorValueAddResponseDto, IndicatorValueEditRequestDto, IndicatorValueEditResponseDto, IndicatorValueDeleteRequestDto, IndicatorValueDeleteResponseDto, IndicatorValueSearchResponseDto>
       , IIndicatorValueService
    {
        public IndicatorValueService(AbstractGenericMapHandler _mapper, AbstractGenericNormalAddService<PmisContext, IndicatorValue, IndicatorValueAddRequestDto, IndicatorValueAddResponseDto> _normalAddService, AbstractGenericNormalEditService<PmisContext, IndicatorValue, IndicatorValueEditRequestDto, IndicatorValueEditResponseDto> _normalEditService, AbstractGenericNormalLogicalDeleteService<PmisContext, IndicatorValue, IndicatorValueDeleteRequestDto, IndicatorValueDeleteResponseDto> _logicalDeleteService, AbstractGenericNormalPhysicalDeleteService<PmisContext, IndicatorValue, IndicatorValueDeleteRequestDto, IndicatorValueDeleteResponseDto> _physicalDeleteService, AbstractGenericNormalSearchService<PmisContext, IndicatorValue, IndicatorValueSearchResponseDto> _searchService) : base(_mapper, _normalAddService, _normalEditService, _logicalDeleteService, _physicalDeleteService, _searchService)
        {
        }
    }
}
