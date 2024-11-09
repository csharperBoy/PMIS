using Generic.Base.Handler.Map;
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
            this.FkLkpCategoryDetailInfo = await (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLkpCategoryDetail);
            this.FkLkpCategoryMasterInfo = await (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLkpCategoryMaster);
            this.FkLkpCategoryTypeInfo = await (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLkpCategoryType);
            return this;
        }
        public virtual IndicatorTinyInfoDto   FkIndicatorInfo { get; set; } = null!;

        public virtual LookUpValueTinyInfoDto FkLkpCategoryDetailInfo { get; set; } = null!;

        public virtual LookUpValueTinyInfoDto FkLkpCategoryMasterInfo { get; set; } = null!;

        public virtual LookUpValueTinyInfoDto FkLkpCategoryTypeInfo { get; set; } = null!;
    }
}
