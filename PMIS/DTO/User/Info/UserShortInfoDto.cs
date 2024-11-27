using Generic.Base.Handler.Map;
using PMIS.DTO.ClaimUserOnSystem.Info;
using PMIS.DTO.ClaimUserOnIndicator.Info;
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
            this.ClaimOnSystemsInfo = await Task.WhenAll(baseModel.ClaimOnSystems.Select(v => (new ClaimUserOnSystemTinyInfoDto()).extraMapFromBaseModel(v)).ToList());

            this.FkLkpWorkCalendarInfo = await (new LookUpValueTinyInfoDto()).extraMapFromBaseModel(baseModel.FkLkpWorkCalendar);
            this.ClaimUserOnIndicatorsInfo = await Task.WhenAll(baseModel.ClaimUserOnIndicators.Select(d => (new ClaimUserOnIndicatorTinyInfoDto()).extraMapFromBaseModel(d)).ToList());

            return this;
        }
        public virtual ICollection<ClaimUserOnSystemTinyInfoDto> ClaimOnSystemsInfo { get; set; } = new List<ClaimUserOnSystemTinyInfoDto>();

        public virtual ICollection<ClaimUserOnIndicatorTinyInfoDto> ClaimUserOnIndicatorsInfo { get; set; } = new List<ClaimUserOnIndicatorTinyInfoDto>();

        public virtual LookUpValueTinyInfoDto FkLkpWorkCalendarInfo { get; set; } = null!;
    }
}
