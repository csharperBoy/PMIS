using Generic.Base.Handler.Map.Abstract;
using Generic.Service.Normal.Composition;
using Generic.Service.Normal.Operation.Abstract;
using PMIS.DTO.LookUp;
using PMIS.DTO.LookUpDestination;
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
        public LookUpDestinationService(AbstractGenericMapHandler _mapper, AbstractGenericNormalAddService<PmisContext, LookUpDestination, LookUpDestinationAddRequestDto, LookUpDestinationAddResponseDto> _normalAddService, AbstractGenericNormalEditService<PmisContext, LookUpDestination, LookUpDestinationEditRequestDto, LookUpDestinationEditResponseDto> _normalEditService, AbstractGenericNormalLogicalDeleteService<PmisContext, LookUpDestination, LookUpDestinationDeleteRequestDto, LookUpDestinationDeleteResponseDto> _logicalDeleteService, AbstractGenericNormalPhysicalDeleteService<PmisContext, LookUpDestination, LookUpDestinationDeleteRequestDto, LookUpDestinationDeleteResponseDto> _physicalDeleteService, AbstractGenericNormalSearchService<PmisContext, LookUpDestination, LookUpDestinationSearchResponseDto> _searchService) : base(_mapper, _normalAddService, _normalEditService, _logicalDeleteService, _physicalDeleteService, _searchService)
        {
        }

        public override async Task<TDestination> ExtraMap<TSource, TDestination>(TSource source, TDestination destination)
        {
            if (destination is LookUpDestination indicatorDestination)
            {
                if (source is LookUpDestinationAddRequestDto addRequesSource)
                {
                    indicatorDestination.SystemInfo = DateTime.Now.ToString();
                }
                else if (source is LookUpDestinationEditRequestDto editRequesSource)
                {
                    indicatorDestination.SystemInfo = DateTime.Now.ToString();
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

                    searchResponsDestination= searchResponsDestination.extraMapFromBaseModel(LookUpSource, searchResponsDestination);
                   
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
