using Generic.Base.Handler.Map;
using PMIS.DTO.ClaimOnSystem.Info;
using PMIS.DTO.Indicator.Info;
using PMIS.DTO.LookUpValue.Info;
using PMIS.DTO.User.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.ClaimOnSystem.Info
{
    public class ClaimOnSystemShortInfoDto : ClaimOnSystemTinyInfoDto
    {

        public async Task<ClaimOnSystemShortInfoDto> extraMapFromBaseModel(PMIS.Models.ClaimOnSystem baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);

            this.FkLkpClaimOnSystemInfo = await (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLkpClaimOnSystem);
            this.FkUserInfo = await (new UserTinyInfoDto()).extraMapFromBaseModel(baseModel.FkUser);
            return this;
        }

        public virtual LookUpValueTinyInfoDto FkLkpClaimOnSystemInfo { get; set; }

        public virtual UserTinyInfoDto FkUserInfo { get; set; }
    }
}
