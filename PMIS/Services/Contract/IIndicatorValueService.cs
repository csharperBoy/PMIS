using Generic.Service.Normal.Composition.Contract;
using PMIS.DTO.IndicatorValue;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services.Contract
{
    public interface IIndicatorValueService : IGenericNormalService<IndicatorValue, IndicatorValueAddRequestDto, IndicatorValueAddResponseDto, IndicatorValueEditRequestDto, IndicatorValueEditResponseDto, IndicatorValueDeleteRequestDto, IndicatorValueDeleteResponseDto, IndicatorValueSearchResponseDto>
    {
        Task<IEnumerable<IndicatorValueSearchResponseDto>> SearchByExternaFilter(IEnumerable<IndicatorValueSearchResponseDto> list, int? lkpFormId, int? lkpPeriodId);
    }
}
