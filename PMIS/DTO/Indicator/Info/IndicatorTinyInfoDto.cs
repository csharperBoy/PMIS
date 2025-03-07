﻿using Generic.Base.Handler.Map;
using Generic.Service.DTO.Concrete;
using PMIS.DTO.ClaimUserOnIndicator.Info;
using PMIS.DTO.IndicatorCategory.Info;
using PMIS.DTO.IndicatorValue.Info;
using PMIS.DTO.LookUpValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.Indicator.Info
{
    public class IndicatorTinyInfoDto : GenericSearchResponseDto
    {
        public async Task<IndicatorTinyInfoDto> extraMapFromBaseModel(PMIS.Models.Indicator baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);
            return this;
        }
        public int Id { get; set; }

        public string Code { get; set; } = null!;
        

        public string Title { get; set; } = null!;       

        public string? Formula { get; set; }

        public string? Description { get; set; }

        public string? SystemInfo { get; set; }

        public bool? FlgLogicalDelete { get; set; }

        public int FkLkpFormId { get; set; }

        public int FkLkpManualityId { get; set; }

        public int FkLkpUnitId { get; set; }

        public int FkLkpPeriodId { get; set; }

        public int FkLkpMeasureId { get; set; }

        public int FkLkpDesirabilityId { get; set; }

    }
}
