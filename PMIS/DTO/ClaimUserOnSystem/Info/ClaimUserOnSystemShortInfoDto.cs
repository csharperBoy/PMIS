using Generic.Base.Handler.Map;
using PMIS.DTO.ClaimUserOnSystem.Info;
using PMIS.DTO.Indicator.Info;
using PMIS.DTO.LookUpValue.Info;
using PMIS.DTO.User.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.ClaimUserOnSystem.Info
{
    public class ClaimUserOnSystemShortInfoDto : ClaimUserOnSystemTinyInfoDto
    {

        public async Task<ClaimUserOnSystemShortInfoDto> extraMapFromBaseModel(PMIS.Models.ClaimUserOnSystem baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);

            this.FkLkpClaimUserOnSystemInfo = await (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLkpClaimUserOnSystem);
            this.FkUserInfo = await (new UserTinyInfoDto()).extraMapFromBaseModel(baseModel.FkUser);
            return this;
        }

        public virtual LookUpValueTinyInfoDto FkLkpClaimUserOnSystemInfo { get; set; }

        public virtual UserTinyInfoDto FkUserInfo { get; set; }
    }
}
