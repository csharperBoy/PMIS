using AutoMapper;
using Generic.Base.Handler.Map.Abstract;
using Generic.Service.Normal.Composition;
using Generic.Service.Normal.Operation.Abstract;
using PMIS.DTO.LookUpValue;
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
    public class LookUpValueService
: GenericNormalService<PmisContext, LookUpValue, LookUpValueAddRequestDto, LookUpValueAddResponseDto, LookUpValueEditRequestDto, LookUpValueEditResponseDto, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto, LookUpValueSearchResponseDto>
       , ILookUpValueService
    {
        public LookUpValueService(AbstractGenericMapHandler _mapper, AbstractGenericNormalAddService<PmisContext, LookUpValue, LookUpValueAddRequestDto, LookUpValueAddResponseDto> _normalAddService, AbstractGenericNormalEditService<PmisContext, LookUpValue, LookUpValueEditRequestDto, LookUpValueEditResponseDto> _normalEditService, AbstractGenericNormalLogicalDeleteService<PmisContext, LookUpValue, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto> _logicalDeleteService, AbstractGenericNormalPhysicalDeleteService<PmisContext, LookUpValue, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto> _physicalDeleteService, AbstractGenericNormalSearchService<PmisContext, LookUpValue, LookUpValueSearchResponseDto> _searchService) : base(_mapper, _normalAddService, _normalEditService, _logicalDeleteService, _physicalDeleteService, _searchService)
        {
        }
    }
}

