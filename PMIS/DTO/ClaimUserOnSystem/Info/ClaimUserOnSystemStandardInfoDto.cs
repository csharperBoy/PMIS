using Generic.Base.Handler.Map;
using PMIS.DTO.LookUpValue.Info;
using PMIS.DTO.User.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.ClaimUserOnSystem.Info
{
    public class ClaimUserOnSystemStandardInfoDto : ClaimUserOnSystemTinyInfoDto
    {

        public async Task<ClaimUserOnSystemStandardInfoDto> extraMapFromBaseModel(PMIS.Models.ClaimUserOnSystem baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);

            this.FkLkpClaimUserOnSystemInfo = await (new LookUpValueShortInfoDto()).extraMapFromBaseModel(baseModel.FkLkpClaimOnSystem);
            this.FkUserInfo = await (new UserShortInfoDto()).extraMapFromBaseModel(baseModel.FkUser);
            return this;
        }

        public virtual LookUpValueShortInfoDto FkLkpClaimUserOnSystemInfo { get; set; }

        public virtual UserShortInfoDto FkUserInfo { get; set; }
    }
}
