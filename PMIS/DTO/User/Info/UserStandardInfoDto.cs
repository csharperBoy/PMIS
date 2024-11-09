using Generic.Base.Handler.Map;
using PMIS.DTO.Claim.Info;
using PMIS.DTO.LookUpValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.User.Info
{
    public class UserStandardInfoDto : UserTinyInfoDto
    {
        public async Task<UserStandardInfoDto> extraMapFromBaseModel(PMIS.Models.User baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);

            this.FkLkpWorkCalenarInfo = await (new LookUpValueShortInfoDto()).extraMapFromBaseModel(baseModel.FkLkpWorkCalenar);
            this.ClaimsInfo = await Task.WhenAll(baseModel.Claims.Select(d => (new ClaimShortInfoDto()).extraMapFromBaseModel(d)).ToList());

            return this;
        }

        public virtual ICollection<ClaimShortInfoDto> ClaimsInfo { get; set; } = new List<ClaimShortInfoDto>();

        public virtual LookUpValueShortInfoDto FkLkpWorkCalenarInfo { get; set; } = null!;
    }
}
