using Generic.Base.Handler.Map;
using PMIS.DTO.Claim.Info;
using PMIS.DTO.IndicatorCategory.Info;
using PMIS.DTO.IndicatorValue.Info;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpDestination.Info;
using PMIS.DTO.LookUpValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.Indicator.Info
{
    public class IndicatorShortInfoDto : IndicatorTinyInfoDto
    {
        public async Task<IndicatorShortInfoDto> extraMapFromBaseModel(PMIS.Models.Indicator baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);
            
            this.FkLkpDesirabilityInfo= await (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLkpDesirability);
            this.FkLkpFormInfo= await (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLkpForm);
            this.FkLkpManualityInfo= await (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLkpManuality);
            this.FkLkpMeasureInfo= await (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLkpMeasure);
            this.FkLkpPeriodInfo= await (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLkpPeriod);
            this.FkLkpUnitInfo= await (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLkpUnit);
            
            this.ClaimsInfo = await Task.WhenAll(baseModel.Claims.Select(d => (new ClaimTinyInfoDto()).extraMapFromBaseModel(d)).ToList());
            this.IndicatorCategoriesInfo =await Task.WhenAll(baseModel.IndicatorCategories.Select(d => (new IndicatorCategoryTinyInfoDto()).extraMapFromBaseModel(d)).ToList());
            this.IndicatorValuesInfo= await Task.WhenAll(baseModel.IndicatorValues.Select(d => (new IndicatorValueTinyInfoDto()).extraMapFromBaseModel(d)).ToList());
            return this;
        }


        public virtual ICollection<ClaimTinyInfoDto> ClaimsInfo { get; set; } = new List<ClaimTinyInfoDto>();

        public virtual LookUpValueTinyInfoDto FkLkpDesirabilityInfo { get; set; } = null!;

        public virtual LookUpValueTinyInfoDto FkLkpFormInfo { get; set; } = null!;

        public virtual LookUpValueTinyInfoDto FkLkpManualityInfo { get; set; } = null!;

        public virtual LookUpValueTinyInfoDto FkLkpMeasureInfo { get; set; } = null!;

        public virtual LookUpValueTinyInfoDto FkLkpPeriodInfo { get; set; } = null!;

        public virtual LookUpValueTinyInfoDto FkLkpUnitInfo { get; set; } = null!;

        public virtual ICollection<IndicatorCategoryTinyInfoDto> IndicatorCategoriesInfo { get; set; } = new List<IndicatorCategoryTinyInfoDto>();

        public virtual ICollection<IndicatorValueTinyInfoDto> IndicatorValuesInfo { get; set; } = new List<IndicatorValueTinyInfoDto>();
    }
}
