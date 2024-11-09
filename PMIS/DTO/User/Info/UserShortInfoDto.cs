using Generic.Base.Handler.Map;
using PMIS.DTO.Claim.Info;
using PMIS.DTO.Indicator.Info;
using PMIS.DTO.IndicatorValue.Info;
using PMIS.DTO.LookUpValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.User.Info
{
    public class UserShortInfoDto : UserTinyInfoDto
    {
        public async Task<UserShortInfoDto> extraMapFromBaseModel(PMIS.Models.User baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);

            this.FkLkpWorkCalenarInfo = await (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLkpWorkCalenar);
            this.ClaimsInfo = await Task.WhenAll(baseModel.Claims.Select(d => (new ClaimTinyInfoDto()).extraMapFromBaseModel(d)).ToList());

            return this;
        }

        public virtual ICollection<ClaimTinyInfoDto> ClaimsInfo { get; set; } = new List<ClaimTinyInfoDto>();

        public virtual LookUpValueTinyInfoDto FkLkpWorkCalenarInfo { get; set; } = null!;
    }
}
