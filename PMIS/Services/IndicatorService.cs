using PMIS.DTO.Indicator;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services
{
    public class IndicatorService //: GenericService<Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto, IndicatorEditRequestDto, IndicatorEditResponseDto, IndicatorDeleteRequestDto, IndicatorDeleteResponseDto, IndicatorSearchRequestDto, IndicatorSearchResponseDto>
    {       
        //protected override Indicator mapAddReqToEntity(IndicatorAddRequestDto reqEntity)
        //{

        //    Indicator model = new Indicator()
        //    {
        //        Code = reqEntity.Code,
        //        Description = reqEntity.Description,
        //        Formula = reqEntity.Formula,
        //        Title = reqEntity.Title,
        //        FkLkpUnitId = reqEntity.FkLkpUnitId,
        //        FkLkpPeriodId = reqEntity.FkLkpPeriodId,
        //        FkLkpMeasureId = reqEntity.FkLkpMeasureId,
        //        FkLkpManualityId = reqEntity.FkLkpManualityId,
        //        FkLkpFormId = reqEntity.FkLkpFormId,
        //        FkLkpDesirabilityId = reqEntity.FkLkpDesirabilityId

        //    };


        //    return model;
        //}
        //protected override IndicatorAddResponseDto mapEntityToAddRes(Indicator entity , string? errorMessage = null)
        //{
        //    IndicatorAddResponseDto model = new IndicatorAddResponseDto()
        //    {
        //        Id = entity.Id      ,
        //        IsSuccess = entity.Id == 0 ? false : true , 
        //        ErrorMessage = errorMessage
        //    };
            
        //    return model;
        //}
        //protected override Indicator mapDeleteReqToEntity(IndicatorDeleteRequestDto reqEntities)
        //{
        //    return base.mapDeleteReqToEntity(reqEntities);
        //}
        //protected override Task<(bool, Expression<Func<Indicator, bool>>, Func<IQueryable<Indicator>, IOrderedQueryable<Indicator>>, string, int, int)> DecodeSearchRequest(IndicatorSearchRequestDto filters)
        //{
        //    return base.DecodeSearchRequest(filters);
        //}
        //protected override Indicator mapEditReqToEntity(IndicatorEditRequestDto reqEntities)
        //{
        //    return base.mapEditReqToEntity(reqEntities);
        //}
        //protected override IEnumerable<IndicatorSearchResponseDto> mapEntitiesToSearchResult(IEnumerable<Indicator> entities, int count)
        //{
        //    return base.mapEntitiesToSearchResult(entities, count);
        //}

        //protected override IndicatorDeleteResponseDto mapEntityToDeleteRes(Indicator entity)
        //{
        //    return base.mapEntityToDeleteRes(entity);
        //}
        //protected override IndicatorEditResponseDto mapEntityToEditRes(Indicator entity)
        //{
        //    return base.mapEntityToEditRes(entity);
        //}
        //protected override Task<IEnumerable<IndicatorSearchResponseDto>> Search(IndicatorSearchRequestDto request)
        //{
        //    return base.Search(request);
        //}
    }
}
