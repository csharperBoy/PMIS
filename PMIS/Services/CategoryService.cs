using Generic.Base.Handler.Map.Abstract;
using Generic.Service.Normal.Composition;
using Generic.Service.Normal.Operation.Abstract;
using PMIS.DTO.Category;
using PMIS.Models;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services
{
    public class CategoryService
    : GenericNormalService<PmisContext, Category, CategoryAddRequestDto, CategoryAddResponseDto, CategoryEditRequestDto, CategoryEditResponseDto, CategoryDeleteRequestDto, CategoryDeleteResponseDto, CategorySearchResponseDto>
       , ICategoryService
    {
        public CategoryService(AbstractGenericMapHandler _mapper, AbstractGenericNormalAddService<PmisContext, Category, CategoryAddRequestDto, CategoryAddResponseDto> _normalAddService, AbstractGenericNormalEditService<PmisContext, Category, CategoryEditRequestDto, CategoryEditResponseDto> _normalEditService, AbstractGenericNormalLogicalDeleteService<PmisContext, Category, CategoryDeleteRequestDto, CategoryDeleteResponseDto> _logicalDeleteService, AbstractGenericNormalPhysicalDeleteService<PmisContext, Category, CategoryDeleteRequestDto, CategoryDeleteResponseDto> _physicalDeleteService, AbstractGenericNormalSearchService<PmisContext, Category, CategorySearchResponseDto> _searchService) : base(_mapper, _normalAddService, _normalEditService, _logicalDeleteService, _physicalDeleteService, _searchService)
        {

        }

    }
}
