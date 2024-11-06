using AutoMapper;
using Generic.Base.Handler.Map.Abstract;
using Generic.Service.Normal.Composition;
using Generic.Service.Normal.Operation.Abstract;
using PMIS.DTO.LookUp;
using PMIS.DTO.LookUpDestination.Info;
using PMIS.DTO.LookUpDestination;
using PMIS.Models;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpValue.Info;
using PMIS.DTO.LookUpValue;

namespace PMIS.Services
{
    public class LookUpService : GenericNormalService<PmisContext, LookUp, LookUpAddRequestDto, LookUpAddResponseDto, LookUpEditRequestDto, LookUpEditResponseDto, LookUpDeleteRequestDto, LookUpDeleteResponseDto, LookUpSearchResponseDto>
        , ILookUpService
    {
        AbstractGenericMapHandler mapper;
        public LookUpService(AbstractGenericMapHandler _mapper, AbstractGenericNormalAddService<PmisContext, LookUp, LookUpAddRequestDto, LookUpAddResponseDto> _normalAddService, AbstractGenericNormalEditService<PmisContext, LookUp, LookUpEditRequestDto, LookUpEditResponseDto> _normalEditService, AbstractGenericNormalLogicalDeleteService<PmisContext, LookUp, LookUpDeleteRequestDto, LookUpDeleteResponseDto> _logicalDeleteService, AbstractGenericNormalPhysicalDeleteService<PmisContext, LookUp, LookUpDeleteRequestDto, LookUpDeleteResponseDto> _physicalDeleteService, AbstractGenericNormalSearchService<PmisContext, LookUp, LookUpSearchResponseDto> _searchService) : base(_mapper, _normalAddService, _normalEditService, _logicalDeleteService, _physicalDeleteService, _searchService)
        {
            mapper = _mapper;
            this.mapper.MappingEvent += ExtraMap;
        }

        public override async Task<TDestination> ExtraMap<TSource, TDestination>(TSource source, TDestination destination)
        {
            if (destination is LookUp indicatorDestination)
            {
                if (source is LookUpAddRequestDto addRequesSource)
                {
                    indicatorDestination.SystemInfo = DateTime.Now.ToString();
                }
                else if (source is LookUpEditRequestDto editRequesSource)
                {
                    indicatorDestination.SystemInfo = DateTime.Now.ToString();
                }
            }
            else if (source is LookUp LookUpSource)
            {
                if (destination is LookUpAddResponseDto addResponsDestination)
                {
                    //addResponsDestination.ErrorMessage = $"{LookUpSource.Code} {LookUpSource.Title}";
                }
                else if (destination is LookUpEditResponseDto editResponsDestination)
                {
                    //editResponsDestination.ErrorMessage = $"{LookUpSource.Code} {LookUpSource.Title}";
                }
                else if (destination is LookUpSearchResponseDto searchResponsDestination)
                {
                    // searchResponsDestination=(LookUpSearchResponseDto)searchResponsDestination.extraMapFromBaseModel(LookUpSource);
                    //destination = searchResponsDestination.extraMapFromBaseModel(LookUpSource) as TDestination;
                    destination = mapper.Map<LookUpStandardInfoDto, LookUpSearchResponseDto>(searchResponsDestination.extraMapFromBaseModel(LookUpSource)) as TDestination;

                }
            }
            else
            {
                //LookUp indicatorIntermediary = new LookUp();
                //indicatorIntermediary = ExtraMap<TSource, TDestination>(source, indicatorIntermediary);
                //destination = ExtraMap<TSource, TDestination>(indicatorIntermediary, destination);
            }
            return await Task.FromResult(destination);
        }

        
    }
}

