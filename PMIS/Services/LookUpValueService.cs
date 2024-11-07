using AutoMapper;
using Generic.Base.Handler.Map.Abstract;
using Generic.Service.Normal.Composition;
using Generic.Service.Normal.Operation.Abstract;
using PMIS.DTO.LookUp;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpValue;
using PMIS.DTO.LookUpValue.Info;
using PMIS.Models;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services
{
    public class LookUpValueService : GenericNormalService<PmisContext, LookUpValue, LookUpValueAddRequestDto, LookUpValueAddResponseDto, LookUpValueEditRequestDto, LookUpValueEditResponseDto, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto, LookUpValueSearchResponseDto>
        , ILookUpValueService
    {
        AbstractGenericMapHandler mapper;
        public LookUpValueService(AbstractGenericMapHandler _mapper, AbstractGenericNormalAddService<PmisContext, LookUpValue, LookUpValueAddRequestDto, LookUpValueAddResponseDto> _normalAddService, AbstractGenericNormalEditService<PmisContext, LookUpValue, LookUpValueEditRequestDto, LookUpValueEditResponseDto> _normalEditService, AbstractGenericNormalLogicalDeleteService<PmisContext, LookUpValue, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto> _logicalDeleteService, AbstractGenericNormalPhysicalDeleteService<PmisContext, LookUpValue, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto> _physicalDeleteService, AbstractGenericNormalSearchService<PmisContext, LookUpValue, LookUpValueSearchResponseDto> _searchService) : base(_mapper, _normalAddService, _normalEditService, _logicalDeleteService, _physicalDeleteService, _searchService)
        {
            mapper = _mapper;
            //this.mapper.MappingEvent += ExtraMap;
        }

        public override async Task<TDestination> ExtraMap<TSource, TDestination>(TSource source, TDestination destination)
        {
            if (destination is LookUpValue indicatorDestination)
            {
                if (source is LookUpValueAddRequestDto addRequesSource)
                {
                    indicatorDestination.SystemInfo = DateTime.Now.ToString();
                }
                else if (source is LookUpValueEditRequestDto editRequesSource)
                {
                    indicatorDestination.SystemInfo = DateTime.Now.ToString();
                }
            }
            else if (source is LookUpValue LookUpSource)
            {
                if (destination is LookUpValueAddResponseDto addResponsDestination)
                {
                    //addResponsDestination.ErrorMessage = $"{LookUpSource.Code} {LookUpSource.Title}";
                }
                else if (destination is LookUpValueEditResponseDto editResponsDestination)
                {
                    //editResponsDestination.ErrorMessage = $"{LookUpSource.Code} {LookUpSource.Title}";
                }
                else if (destination is LookUpValueSearchResponseDto searchResponsDestination)
                {
                    // searchResponsDestination =(LookUpValueSearchResponseDto) searchResponsDestination.extraMapFromBaseModel(LookUpSource);
                    destination = mapper.Map<LookUpValueStandardInfoDto ,LookUpValueSearchResponseDto>( searchResponsDestination.extraMapFromBaseModel(LookUpSource)) as TDestination;
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

