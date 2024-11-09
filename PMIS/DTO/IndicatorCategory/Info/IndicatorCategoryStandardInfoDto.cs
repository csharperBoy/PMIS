using Generic.Base.Handler.Map;
using PMIS.DTO.Indicator.Info;
using PMIS.DTO.LookUpValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.IndicatorCategory.Info
{
    public class IndicatorCategoryStandardInfoDto: IndicatorCategoryTinyInfoDto
    {
        public async Task<IndicatorCategoryStandardInfoDto> extraMapFromBaseModel(PMIS.Models.IndicatorCategory baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);

            this.FkIndicatorInfo = await (new IndicatorShortInfoDto()).extraMapFromBaseModel(baseModel.FkIndicator);
            this.FkLkpCategoryDetailInfo = await (new LookUpValueShortInfoDto()).extraMapFromBaseModel(baseModel.FkLkpCategoryDetail);
            this.FkLkpCategoryMasterInfo = await (new LookUpValueShortInfoDto()).extraMapFromBaseModel(baseModel.FkLkpCategoryMaster);
            this.FkLkpCategoryTypeInfo = await (new LookUpValueShortInfoDto()).extraMapFromBaseModel(baseModel.FkLkpCategoryType);
            return this;
        }
        public virtual IndicatorShortInfoDto FkIndicatorInfo { get; set; } = null!;

        public virtual LookUpValueShortInfoDto FkLkpCategoryDetailInfo { get; set; } = null!;

        public virtual LookUpValueShortInfoDto FkLkpCategoryMasterInfo { get; set; } = null!;

        public virtual LookUpValueShortInfoDto FkLkpCategoryTypeInfo { get; set; } = null!;
    }
}
