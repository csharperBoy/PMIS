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
        : GenericNormalService<PmisContext,Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto, IndicatorEditRequestDto, IndicatorEditResponseDto, IndicatorDeleteRequestDto, IndicatorDeleteResponseDto, IndicatorSearchResponseDto>
        , IIndicatorService
    {
        public IndicatorService(AbstractGenericMapHandler _mapper, AbstractGenericNormalAddService<PmisContext, Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto> _normalAddService, AbstractGenericNormalEditService<PmisContext, Indicator, IndicatorEditRequestDto, IndicatorEditResponseDto> _normalEditService, AbstractGenericNormalLogicalDeleteService<PmisContext, Indicator, IndicatorDeleteRequestDto, IndicatorDeleteResponseDto> _logicalDeleteService, AbstractGenericNormalPhysicalDeleteService<PmisContext, Indicator, IndicatorDeleteRequestDto, IndicatorDeleteResponseDto> _physicalDeleteService, AbstractGenericNormalSearchService<PmisContext, Indicator, IndicatorSearchResponseDto> _searchService) : base(_mapper, _normalAddService, _normalEditService, _logicalDeleteService, _physicalDeleteService, _searchService)
        {
        }

        public override async Task<TDestination> ExtraMap<TSource, TDestination>(TSource source, TDestination destination)
        {
            if (destination is Indicator indicatorDestination)
            {
                if (source is IndicatorAddRequestDto addRequesSource)
                {
                    indicatorDestination.SystemInfo = DateTime.Now.ToString();
                }
                else if (source is IndicatorEditRequestDto editRequesSource)
                {
                    indicatorDestination.SystemInfo = DateTime.Now.ToString();
                }
            }
            else if (source is Indicator IndicatorSource)
            {
                if (destination is IndicatorAddResponseDto addResponsDestination)
                {
                    addResponsDestination.ErrorMessage = $"{IndicatorSource.Code} {IndicatorSource.Title}";
                }
                else if (destination is IndicatorEditResponseDto editResponsDestination)
                {
                    editResponsDestination.ErrorMessage = $"{IndicatorSource.Code} {IndicatorSource.Title}";
                }
            }
            else
            {
                //Indicator indicatorIntermediary = new Indicator();
                //indicatorIntermediary = ExtraMap<TSource, TDestination>(source, indicatorIntermediary);
                //destination = ExtraMap<TSource, TDestination>(indicatorIntermediary, destination);
            }
            return await Task.FromResult(destination);
        }
    }
}