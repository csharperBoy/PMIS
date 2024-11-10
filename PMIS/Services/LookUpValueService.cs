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
using Generic.Service.DTO;
using PMIS.DTO.LookUpDestination;
using Generic.Base.Handler.Map;

namespace PMIS.Services
{
    public class LookUpValueService
: GenericNormalService<PmisContext, LookUpValue, LookUpValueAddRequestDto, LookUpValueAddResponseDto, LookUpValueEditRequestDto, LookUpValueEditResponseDto, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto, LookUpValueSearchResponseDto>
       , ILookUpValueService
    {
        ILookUpDestinationService lookUpDestinationService;
        public LookUpValueService(AbstractGenericMapHandler _mapper, AbstractGenericNormalAddService<PmisContext, LookUpValue, LookUpValueAddRequestDto, LookUpValueAddResponseDto> _normalAddService, AbstractGenericNormalEditService<PmisContext, LookUpValue, LookUpValueEditRequestDto, LookUpValueEditResponseDto> _normalEditService, AbstractGenericNormalLogicalDeleteService<PmisContext, LookUpValue, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto> _logicalDeleteService, AbstractGenericNormalPhysicalDeleteService<PmisContext, LookUpValue, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto> _physicalDeleteService, AbstractGenericNormalSearchService<PmisContext, LookUpValue, LookUpValueSearchResponseDto> _searchService, ILookUpDestinationService lookUpDestinationService) : base(_mapper, _normalAddService, _normalEditService, _logicalDeleteService, _physicalDeleteService, _searchService)
        {
            this.lookUpDestinationService = lookUpDestinationService;
        }
        public async Task<List<LookUpValueShortInfoDto>> GetList(string _tableName, string _columnName, string _code)
        {
            try
            {


                GenericSearchRequestDto req = new GenericSearchRequestDto()
                {
                    filters = new List<GenericSearchFilterDto>() {
                     new GenericSearchFilterDto() {
                    columnName = "TableName",
                    LogicalOperator = LogicalOperator.Begin,
                    operation = FilterOperator.Equals,
                    type = PhraseType.Condition,
                    value = _tableName
                    },
                  new GenericSearchFilterDto() {
                    columnName = "ColumnName",
                    LogicalOperator = LogicalOperator.And,
                    operation = FilterOperator.Equals,
                    type = PhraseType.Condition,
                    value = _columnName
                    },

                }
                };
                (bool IsSuccess, IEnumerable<LookUpDestinationSearchResponseDto> list) = await lookUpDestinationService.Search(req);
                list = list.Where(l => l.FkLookUpInfo.Code == _code);

                return list.Single().LookUpValuesInfo.ToList();
                //List<LookUpValue> lst1 = list.Single().LookUpValuesInfo.Select(h =>
                //                GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map<LookUpValueShortInfoDto, LookUpValue>(
                //                h
                //                ).Result
                //            ).ToList();
                //List<LookUpValueSearchResponseDto> lst2 = lst1.Select(h =>
                //            GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map<LookUpValue, LookUpValueSearchResponseDto>(
                //                h
                //                ).Result
                //            ).ToList();
                //List<LookUpValueSearchResponseDto> result = list.Single().LookUpValuesInfo.Select(h =>
                //            GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map<LookUpValue, LookUpValueSearchResponseDto>(
                //                GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map<LookUpValueShortInfoDto, LookUpValue>(
                //                h
                //                ).Result
                //                ).Result
                            
                //            ).ToList();
                //return lst2;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

