using AutoMapper;
using Generic.Base.Handler.Map.Abstract;
using Generic.Service.Normal.Composition;
using Generic.Service.Normal.Operation.Abstract;
using PMIS.DTO.LookUp;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.LookUpDestination.Info;
using PMIS.DTO.LookUpValue.Info;
using PMIS.DTO.LookUpValue;
using PMIS.Models;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services
{
    public class LookUpDestinationService : GenericNormalService<PmisContext, LookUpDestination, LookUpDestinationAddRequestDto, LookUpDestinationAddResponseDto, LookUpDestinationEditRequestDto, LookUpDestinationEditResponseDto, LookUpDestinationDeleteRequestDto, LookUpDestinationDeleteResponseDto, LookUpDestinationSearchResponseDto>
       , ILookUpDestinationService
    {
        AbstractGenericMapHandler mapper;
        public LookUpDestinationService(AbstractGenericMapHandler _mapper, AbstractGenericNormalAddService<PmisContext, LookUpDestination, LookUpDestinationAddRequestDto, LookUpDestinationAddResponseDto> _normalAddService, AbstractGenericNormalEditService<PmisContext, LookUpDestination, LookUpDestinationEditRequestDto, LookUpDestinationEditResponseDto> _normalEditService, AbstractGenericNormalLogicalDeleteService<PmisContext, LookUpDestination, LookUpDestinationDeleteRequestDto, LookUpDestinationDeleteResponseDto> _logicalDeleteService, AbstractGenericNormalPhysicalDeleteService<PmisContext, LookUpDestination, LookUpDestinationDeleteRequestDto, LookUpDestinationDeleteResponseDto> _physicalDeleteService, AbstractGenericNormalSearchService<PmisContext, LookUpDestination, LookUpDestinationSearchResponseDto> _searchService) : base(_mapper, _normalAddService, _normalEditService, _logicalDeleteService, _physicalDeleteService, _searchService)
        {
             mapper = _mapper;
            //_mapper.ExtraMap += ExtraMap;
            //this.mapper.MappingEvent += ExtraMap;
        }

        public override async Task<TDestination> ExtraMap<TSource, TDestination>(TSource source, TDestination destination)
        {
            if (destination is LookUpDestination lookUpDestinationDestination)
            {
                if (source is LookUpDestinationAddRequestDto addRequesSource)
                {
                    lookUpDestinationDestination.SystemInfo = DateTime.Now.ToString();
                }
                else if (source is LookUpDestinationEditRequestDto editRequesSource)
                {
                    lookUpDestinationDestination.SystemInfo = DateTime.Now.ToString();
                }
            }
            else if (source is LookUpDestination LookUpSource)
            {
                if (destination is LookUpDestinationAddResponseDto addResponsDestination)
                {
                    //addResponsDestination.ErrorMessage = $"{LookUpSource.Code} {LookUpSource.Title}";
                }
                else if (destination is LookUpDestinationEditResponseDto editResponsDestination)
                {
                    //editResponsDestination.ErrorMessage = $"{LookUpSource.Code} {LookUpSource.Title}";
                }
                else if (destination is LookUpDestinationSearchResponseDto searchResponsDestination)
                {
                    //var a = searchResponsDestination.extraMapFromBaseModel(LookUpSource) ;
                    //if(a is LookUpDestinationStandardInfoDto)
                    //{
                    //    var b = a as LookUpDestinationSearchResponseDto;
                    //    destination = b as TDestination;
                    //}
                    // destination = (searchResponsDestination.extraMapFromBaseModel(LookUpSource) as LookUpDestinationSearchResponseDto) as TDestination;            

                    searchResponsDestination =await mapper.Map<LookUpDestinationStandardInfoDto, LookUpDestinationSearchResponseDto>(searchResponsDestination.extraMapFromBaseModel(LookUpSource));
                }
            }
            else
            {
               
            }
            return await Task.FromResult(destination);
        }


    }
}
