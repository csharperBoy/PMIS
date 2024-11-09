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

namespace PMIS.DTO.Claim.Info
{
    public class ClaimShortInfoDto: ClaimTinyInfoDto
    {
        public async Task<ClaimShortInfoDto> extraMapFromBaseModel(PMIS.Models.Claim baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);

            this.FkIndicatorInfo = await (new IndicatorTinyInfoDto()).extraMapFromBaseModel(baseModel.FkIndicator);
            this.FkLkpClaimInfo = await (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLkpClaim);
            this.FkUserInfo = await (new UserTinyInfoDto()).extraMapFromBaseModel(baseModel.FkUser);
            return this;
        }



        public virtual IndicatorTinyInfoDto FkIndicatorInfo { get; set; } = null!;

        public virtual LookUpValueTinyInfoDto FkLkpClaimInfo { get; set; } = null!;

        public virtual UserTinyInfoDto FkUserInfo { get; set; } = null!;
    }
}
