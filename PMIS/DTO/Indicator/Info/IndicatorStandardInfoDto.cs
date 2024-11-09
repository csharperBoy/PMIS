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
    public class IndicatorStandardInfoDto : IndicatorTinyInfoDto
    {
        public async Task<IndicatorStandardInfoDto> extraMapFromBaseModel(PMIS.Models.Indicator baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);

            this.FkLkpDesirabilityInfo = await (new LookUpValueShortInfoDto()).extraMapFromBaseModel(baseModel.FkLkpDesirability);
            this.FkLkpFormInfo = await (new LookUpValueShortInfoDto()).extraMapFromBaseModel(baseModel.FkLkpForm);
            this.FkLkpManualityInfo = await (new LookUpValueShortInfoDto()).extraMapFromBaseModel(baseModel.FkLkpManuality);
            this.FkLkpMeasureInfo = await (new LookUpValueShortInfoDto()).extraMapFromBaseModel(baseModel.FkLkpMeasure);
            this.FkLkpPeriodInfo = await (new LookUpValueShortInfoDto()).extraMapFromBaseModel(baseModel.FkLkpPeriod);
            this.FkLkpUnitInfo = await (new LookUpValueShortInfoDto()).extraMapFromBaseModel(baseModel.FkLkpUnit);

            this.ClaimsInfo = await Task.WhenAll(baseModel.Claims.Select(d => (new ClaimShortInfoDto()).extraMapFromBaseModel(d)).ToList());
            this.IndicatorCategoriesInfo = await Task.WhenAll(baseModel.IndicatorCategories.Select(d => (new IndicatorCategoryShortInfoDto()).extraMapFromBaseModel(d)).ToList());
            this.IndicatorValuesInfo = await Task.WhenAll(baseModel.IndicatorValues.Select(d => (new IndicatorValueShortInfoDto()).extraMapFromBaseModel(d)).ToList());
            return this;
        }



        public virtual ICollection<ClaimShortInfoDto> ClaimsInfo { get; set; } = new List<ClaimShortInfoDto>();

        public virtual LookUpValueShortInfoDto FkLkpDesirabilityInfo { get; set; } = null!;

        public virtual LookUpValueShortInfoDto FkLkpFormInfo { get; set; } = null!;

        public virtual LookUpValueShortInfoDto FkLkpManualityInfo { get; set; } = null!;

        public virtual LookUpValueShortInfoDto FkLkpMeasureInfo { get; set; } = null!;

        public virtual LookUpValueShortInfoDto FkLkpPeriodInfo { get; set; } = null!;

        public virtual LookUpValueShortInfoDto FkLkpUnitInfo { get; set; } = null!;

        public virtual ICollection<IndicatorCategoryShortInfoDto> IndicatorCategoriesInfo { get; set; } = new List<IndicatorCategoryShortInfoDto>();

        public virtual ICollection<IndicatorValueShortInfoDto> IndicatorValuesInfo { get; set; } = new List<IndicatorValueShortInfoDto>();
    }
}
