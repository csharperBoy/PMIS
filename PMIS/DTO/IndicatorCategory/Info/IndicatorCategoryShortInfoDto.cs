using Generic.Base.Handler.Map;
using PMIS.DTO.Category.Info;
using PMIS.DTO.ClaimUserOnIndicator.Info;
using PMIS.DTO.Indicator.Info;
using PMIS.DTO.IndicatorValue.Info;
using PMIS.DTO.LookUpValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.IndicatorCategory.Info
{
    public class IndicatorCategoryShortInfoDto : IndicatorCategoryTinyInfoDto
    {

        public async Task<IndicatorCategoryShortInfoDto> extraMapFromBaseModel(PMIS.Models.IndicatorCategory baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);

            this.FkIndicatorInfo = await (new IndicatorTinyInfoDto()).extraMapFromBaseModel(baseModel.FkIndicator);
            this.FkCategoryInfo = await (new CategoryTinyInfoDto()).extraMapFromBaseModel(baseModel.FkCategory);
            return this;
        }
        public virtual IndicatorTinyInfoDto   FkIndicatorInfo { get; set; } = null!;


        public virtual CategoryTinyInfoDto FkCategoryInfo { get; set; } = null!;



    }
}
