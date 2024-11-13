﻿using Generic.Base.Handler.Map;
using PMIS.DTO.LookUpValue.Info;
using PMIS.DTO.User.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.ClaimOnSystem.Info
{
    public class ClaimOnSystemStandardInfoDto : ClaimOnSystemTinyInfoDto
    {

        public async Task<ClaimOnSystemStandardInfoDto> extraMapFromBaseModel(PMIS.Models.ClaimOnSystem baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);

            this.FkLkpClaimOnSystemInfo = await (new LookUpValueShortInfoDto()).extraMapFromBaseModel(baseModel.FkLkpClaimOnSystem);
            this.FkUserInfo = await (new UserShortInfoDto()).extraMapFromBaseModel(baseModel.FkUser);
            return this;
        }

        public virtual LookUpValueShortInfoDto FkLkpClaimOnSystemInfo { get; set; }

        public virtual UserShortInfoDto FkUserInfo { get; set; }
    }
}