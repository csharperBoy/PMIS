using Generic.Base.Handler.Map;
using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Repository.Abstract;
using Generic.Service.Normal.Composition;
using Generic.Service.Normal.Operation.Abstract;
using Microsoft.EntityFrameworkCore;
using PMIS.DTO.Indicator;
using PMIS.Models;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services
{
    public class IndicatorService
        : GenericNormalService<PmisContext, Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto, IndicatorEditRequestDto, IndicatorEditResponseDto>
        ,IIndicatorService
    {
        public IndicatorService(AbstractGenericMapHandler _mapper, AbstractGenericNormalAddService<PmisContext, Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto> _normalAddService, AbstractGenericNormalEditService<PmisContext, Indicator, IndicatorEditRequestDto, IndicatorEditResponseDto> _normalEditService) : base(_mapper, _normalAddService, _normalEditService)
        {
        }

        public override async Task<TDestination> ExtraMap<TSource, TDestination>(TSource source, TDestination destination)
        {
            if (source is IndicatorAddRequestDto src1 && destination is Indicator dest1) 
            {
                dest1.SystemInfo = DateTime.Now.ToString();
            }
            else if(source is Indicator src2 && destination is IndicatorAddResponseDto dest2)
            {
                dest2.ErrorMessage = $"{src2.Code} {src2.Title}";
            }
            return await Task.FromResult(destination);
        }
    }
}
