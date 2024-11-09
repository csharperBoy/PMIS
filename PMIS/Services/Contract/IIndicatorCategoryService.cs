using Generic.Service.Normal.Composition.Contract;
using PMIS.DTO.ClaimUserOnIndicator;
using PMIS.DTO.IndicatorCategory;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services.Contract
{
    public interface IIndicatorCategoryService : IGenericNormalService<IndicatorCategory, IndicatorCategoryAddRequestDto, IndicatorCategoryAddResponseDto, IndicatorCategoryEditRequestDto, IndicatorCategoryEditResponseDto, IndicatorCategoryDeleteRequestDto, IndicatorCategoryDeleteResponseDto, IndicatorCategorySearchResponseDto>
    {
    }
}
