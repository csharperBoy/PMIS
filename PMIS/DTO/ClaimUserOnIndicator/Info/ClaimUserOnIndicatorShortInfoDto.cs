using Generic.Base.Handler.Map;
using PMIS.DTO.Indicator.Info;
using PMIS.DTO.IndicatorCategory.Info;
using PMIS.DTO.IndicatorValue.Info;
using PMIS.DTO.LookUpValue.Info;
using PMIS.DTO.User.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.ClaimUserOnIndicator.Info
{
    public class ClaimUserOnIndicatorShortInfoDto: ClaimUserOnIndicatorTinyInfoDto
    {
        public async Task<ClaimUserOnIndicatorShortInfoDto> extraMapFromBaseModel(PMIS.Models.ClaimUserOnIndicator baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);

            this.FkIndicatorInfo = await (new IndicatorTinyInfoDto()).extraMapFromBaseModel(baseModel.FkIndicator);
            this.FkLkpClaimUserOnIndicatorInfo = await (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLkpClaimUserOnIndicator);
            this.FkUserInfo = await (new UserTinyInfoDto()).extraMapFromBaseModel(baseModel.FkUser);
            return this;
        }



        public virtual IndicatorTinyInfoDto FkIndicatorInfo { get; set; } = null!;

        public virtual LookUpValueTinyInfoDto FkLkpClaimUserOnIndicatorInfo { get; set; } = null!;

        public virtual UserTinyInfoDto FkUserInfo { get; set; } = null!;
    }
}
