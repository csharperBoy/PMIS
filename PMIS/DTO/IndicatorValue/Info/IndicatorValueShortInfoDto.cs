using Generic.Base.Handler.Map;
using PMIS.DTO.ClaimUserOnIndicator.Info;
using PMIS.DTO.Indicator.Info;
using PMIS.DTO.IndicatorCategory.Info;
using PMIS.DTO.LookUpValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.IndicatorValue.Info
{
    public class IndicatorValueShortInfoDto : IndicatorValueTinyInfoDto
    {
        public async Task<IndicatorValueShortInfoDto> extraMapFromBaseModel(PMIS.Models.IndicatorValue baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);

            this.FkIndicatorInfo = await (new IndicatorTinyInfoDto()).extraMapFromBaseModel(baseModel.FkIndicator);
            this.FkLkpShiftInfo = await (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLkpShift);
            this.FkLkpValueTypeInfo = await (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLkpValueType);
            return this;
        }

        public virtual IndicatorTinyInfoDto FkIndicatorInfo { get; set; } = null!;

        public virtual LookUpValueTinyInfoDto FkLkpShiftInfo { get; set; } = null!;

        public virtual LookUpValueTinyInfoDto FkLkpValueTypeInfo { get; set; } = null!;
    }
}
