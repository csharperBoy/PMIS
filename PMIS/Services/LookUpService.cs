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
using PMIS.DTO.LookUp;

namespace PMIS.Services
{
    public class LookUpService
        : GenericNormalService<PmisContext, LookUp, LookUpAddRequestDto, LookUpAddResponseDto, LookUpEditRequestDto, LookUpEditResponseDto, LookUpDeleteRequestDto, LookUpDeleteResponseDto, LookUpSearchResponseDto>
       , ILookUpService
    {
        public LookUpService(AbstractGenericMapHandler _mapper, AbstractGenericNormalAddService<PmisContext, LookUp, LookUpAddRequestDto, LookUpAddResponseDto> _normalAddService, AbstractGenericNormalEditService<PmisContext, LookUp, LookUpEditRequestDto, LookUpEditResponseDto> _normalEditService, AbstractGenericNormalLogicalDeleteService<PmisContext, LookUp, LookUpDeleteRequestDto, LookUpDeleteResponseDto> _logicalDeleteService, AbstractGenericNormalPhysicalDeleteService<PmisContext, LookUp, LookUpDeleteRequestDto, LookUpDeleteResponseDto> _physicalDeleteService, AbstractGenericNormalSearchService<PmisContext, LookUp, LookUpSearchResponseDto> _searchService) : base(_mapper, _normalAddService, _normalEditService, _logicalDeleteService, _physicalDeleteService, _searchService)
        {
        }
    }

}


