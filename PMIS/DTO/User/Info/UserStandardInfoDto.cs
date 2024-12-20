﻿using Generic.Base.Handler.Map;
using PMIS.DTO.ClaimUserOnSystem.Info;
using PMIS.DTO.ClaimUserOnIndicator.Info;
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
            this.ClaimUserOnSystemsInfo = await Task.WhenAll(baseModel.ClaimUserOnSystems.Select(v => (new ClaimUserOnSystemShortInfoDto()).extraMapFromBaseModel(v)).ToList());

            this.FkLkpWorkCalendarInfo = await (new LookUpValueShortInfoDto()).extraMapFromBaseModel(baseModel.FkLkpWorkCalendar);
           

            this.ClaimUserOnIndicatorsInfo = await Task.WhenAll(baseModel.ClaimUserOnIndicators.Select(d => (new ClaimUserOnIndicatorShortInfoDto()).extraMapFromBaseModel(d)).ToList());

            return this;
        }
        public virtual ICollection<ClaimUserOnSystemShortInfoDto> ClaimUserOnSystemsInfo { get; set; } = new List<ClaimUserOnSystemShortInfoDto>();

        public virtual ICollection<ClaimUserOnIndicatorShortInfoDto> ClaimUserOnIndicatorsInfo { get; set; } = new List<ClaimUserOnIndicatorShortInfoDto>();

        public virtual LookUpValueShortInfoDto FkLkpWorkCalendarInfo { get; set; } = null!;
    }
}
