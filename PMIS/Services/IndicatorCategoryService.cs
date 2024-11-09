using Generic.Base.Handler.Map.Abstract;
using Generic.Service.Normal.Composition;
using Generic.Service.Normal.Operation.Abstract;
using PMIS.DTO.IndicatorCategory;
using PMIS.Models;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services
{
    public class IndicatorCategoryService 
        : GenericNormalService<PmisContext, IndicatorCategory, IndicatorCategoryAddRequestDto, IndicatorCategoryAddResponseDto, IndicatorCategoryEditRequestDto, IndicatorCategoryEditResponseDto, IndicatorCategoryDeleteRequestDto, IndicatorCategoryDeleteResponseDto, IndicatorCategorySearchResponseDto>
       , IIndicatorCategoryService
    {
        public IndicatorCategoryService(AbstractGenericMapHandler _mapper, AbstractGenericNormalAddService<PmisContext, IndicatorCategory, IndicatorCategoryAddRequestDto, IndicatorCategoryAddResponseDto> _normalAddService, AbstractGenericNormalEditService<PmisContext, IndicatorCategory, IndicatorCategoryEditRequestDto, IndicatorCategoryEditResponseDto> _normalEditService, AbstractGenericNormalLogicalDeleteService<PmisContext, IndicatorCategory, IndicatorCategoryDeleteRequestDto, IndicatorCategoryDeleteResponseDto> _logicalDeleteService, AbstractGenericNormalPhysicalDeleteService<PmisContext, IndicatorCategory, IndicatorCategoryDeleteRequestDto, IndicatorCategoryDeleteResponseDto> _physicalDeleteService, AbstractGenericNormalSearchService<PmisContext, IndicatorCategory, IndicatorCategorySearchResponseDto> _searchService) : base(_mapper, _normalAddService, _normalEditService, _logicalDeleteService, _physicalDeleteService, _searchService)
        {
        }
    }
}
