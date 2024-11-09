using Generic.Base.Handler.Map;
using PMIS.DTO.Indicator.Info;
using PMIS.DTO.LookUpValue.Info;
using PMIS.DTO.User.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.Claim.Info
{
    public class ClaimStandardInfoDto: ClaimTinyInfoDto
    {
        public async Task<ClaimStandardInfoDto> extraMapFromBaseModel(PMIS.Models.Claim baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);

            this.FkIndicatorInfo = await (new IndicatorShortInfoDto()).extraMapFromBaseModel(baseModel.FkIndicator);
            this.FkLkpClaimInfo = await (new LookUpValueShortInfoDto()).extraMapFromBaseModel(baseModel.FkLkpClaim);
            this.FkUserInfo = await (new UserShortInfoDto()).extraMapFromBaseModel(baseModel.FkUser);
            return this;
        }



        public virtual IndicatorShortInfoDto FkIndicatorInfo { get; set; } = null!;

        public virtual LookUpValueShortInfoDto FkLkpClaimInfo { get; set; } = null!;

        public virtual UserShortInfoDto FkUserInfo { get; set; } = null!;
    }
}
