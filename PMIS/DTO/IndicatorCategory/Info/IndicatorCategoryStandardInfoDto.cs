using Generic.Base.Handler.Map;
using PMIS.DTO.Category.Info;
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
            this.FkCategoryInfo = await (new CategoryShortInfoDto()).extraMapFromBaseModel(baseModel.FkCategory);
            return this;
        }
        public virtual IndicatorShortInfoDto FkIndicatorInfo { get; set; } = null!;

        public virtual CategoryShortInfoDto FkCategoryInfo { get; set; } = null!;


    }
}
