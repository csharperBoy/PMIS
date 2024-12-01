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
using PMIS.DTO.LookUpDestination;
using Generic.Base.Handler.Map;
using Generic.Service.DTO.Abstract;
using Generic.Service.DTO.Concrete;

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
        public async Task<IEnumerable<LookUpDestinationSearchResponseDto>> GetList(string _tableName)
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
                        }
                    }
                };
                (bool IsSuccess, IEnumerable<LookUpDestinationSearchResponseDto> list) = await lookUpDestinationService.Search(req);

                return list.Where(x => x.FlgLogicalDelete != true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<LookUpValueShortInfoDto>> GetList(string _tableName, string _columnName, string _code)
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
                        }
                    }
                };
                (bool IsSuccess, IEnumerable<LookUpDestinationSearchResponseDto> list) = await lookUpDestinationService.Search(req);
                list = list.Where(l => l.FkLookUpInfo.Code == _code && l.FlgLogicalDelete != true);

                return list.Single().LookUpValuesInfo.Where(x => x.FlgLogicalDelete != true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<LookUpValueShortInfoDto>> GetList(IEnumerable<LookUpDestinationSearchResponseDto> _tablelookUpList, string _columnName, string _code)
        {
            try
            {
                IEnumerable<LookUpDestinationSearchResponseDto> result = _tablelookUpList.Where(l => l.ColumnName == _columnName && l.FkLookUpInfo.Code == _code && l.FkLookUpInfo.FlgLogicalDelete != true);
                return await Task.FromResult(result.Single().LookUpValuesInfo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<LookUpShortInfoDto>> GetList(IEnumerable<LookUpDestinationSearchResponseDto> _tablelookUpList, string _code)
        {
            try
            {
                IEnumerable<LookUpDestinationSearchResponseDto> result = _tablelookUpList.Where(l => l.FkLookUpInfo.Code == _code && l.FkLookUpInfo.FlgLogicalDelete != true);

                return await Task.FromResult(result.Select(x => x.FkLookUpInfo).Where(l => l.FlgLogicalDelete != true));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

