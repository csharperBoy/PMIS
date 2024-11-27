﻿using Generic.Base.Handler.Map.Abstract;
using Generic.Service.Normal.Composition;
using Generic.Service.Normal.Operation.Abstract;
using PMIS.Bases;
using PMIS.DTO.ClaimOnSystem;
using PMIS.DTO.Indicator;
using PMIS.Models;
using PMIS.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services
{
    public class ClaimOnSystemService : GenericNormalService<PmisContext, ClaimOnSystem, ClaimOnSystemAddRequestDto, ClaimOnSystemAddResponseDto, ClaimOnSystemEditRequestDto, ClaimOnSystemEditResponseDto, ClaimOnSystemDeleteRequestDto, ClaimOnSystemDeleteResponseDto, ClaimOnSystemSearchResponseDto>
       , IClaimOnSystemService
    {
        public ClaimOnSystemService(AbstractGenericMapHandler _mapper, AbstractGenericNormalAddService<PmisContext, ClaimOnSystem, ClaimOnSystemAddRequestDto, ClaimOnSystemAddResponseDto> _normalAddService, AbstractGenericNormalEditService<PmisContext, ClaimOnSystem, ClaimOnSystemEditRequestDto, ClaimOnSystemEditResponseDto> _normalEditService, AbstractGenericNormalLogicalDeleteService<PmisContext, ClaimOnSystem, ClaimOnSystemDeleteRequestDto, ClaimOnSystemDeleteResponseDto> _logicalDeleteService, AbstractGenericNormalPhysicalDeleteService<PmisContext, ClaimOnSystem, ClaimOnSystemDeleteRequestDto, ClaimOnSystemDeleteResponseDto> _physicalDeleteService, AbstractGenericNormalSearchService<PmisContext, ClaimOnSystem, ClaimOnSystemSearchResponseDto> _searchService) : base(_mapper, _normalAddService, _normalEditService, _logicalDeleteService, _physicalDeleteService, _searchService)
        {
        }
        public async Task<IEnumerable<ClaimOnSystemSearchResponseDto>> GetCurrentUserClaims()
        {
            try
            {
                (bool isSuccess, IEnumerable<ClaimOnSystemSearchResponseDto> result) = await Search(new Generic.Service.DTO.Concrete.GenericSearchRequestDto()
                {
                    filters = new List<Generic.Service.DTO.Concrete.GenericSearchFilterDto>()
                    {
                        new Generic.Service.DTO.Concrete.GenericSearchFilterDto()
                        {
                            columnName = "FkUserId",
                            value = GlobalVariable.userId.ToString(),
                            LogicalOperator=Generic.Service.DTO.Concrete.LogicalOperator.Begin,
                            operation = Generic.Service.DTO.Concrete.FilterOperator.Equals,
                            type = Generic.Service.DTO.Concrete.PhraseType.Condition
                        },
                        new Generic.Service.DTO.Concrete.GenericSearchFilterDto()
                        {
                            columnName = "FlgLogicalDelete",
                            value = false.ToString(),
                            LogicalOperator=Generic.Service.DTO.Concrete.LogicalOperator.And,
                            operation = Generic.Service.DTO.Concrete.FilterOperator.Equals,
                            type = Generic.Service.DTO.Concrete.PhraseType.Condition
                        }
                    }
                });
                if (isSuccess)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
