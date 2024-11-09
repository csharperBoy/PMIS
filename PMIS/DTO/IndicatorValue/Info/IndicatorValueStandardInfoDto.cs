using Generic.Base.Handler.Map;
using PMIS.DTO.Indicator.Info;
using PMIS.DTO.LookUpValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.IndicatorValue.Info
{
    public class IndicatorValueStandardInfoDto: IndicatorValueTinyInfoDto
    {
        public async Task<IndicatorValueStandardInfoDto> extraMapFromBaseModel(PMIS.Models.IndicatorValue baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);

            this.FkIndicatorInfo = await (new IndicatorShortInfoDto()).extraMapFromBaseModel(baseModel.FkIndicator);
            this.FkLkpShiftInfo = await (new LookUpValueShortInfoDto()).extraMapFromBaseModel(baseModel.FkLkpShift);
            this.FkLkpValueTypeInfo = await (new LookUpValueShortInfoDto()).extraMapFromBaseModel(baseModel.FkLkpValueType);
            return this;
        }

        public virtual IndicatorShortInfoDto FkIndicatorInfo { get; set; } = null!;

        public virtual LookUpValueShortInfoDto FkLkpShiftInfo { get; set; } = null!;

        public virtual LookUpValueShortInfoDto FkLkpValueTypeInfo { get; set; } = null!;
    }
}
