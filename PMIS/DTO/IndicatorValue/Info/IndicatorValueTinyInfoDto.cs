﻿using Generic.Base.Handler.Map;
using Generic.Helper;
using Generic.Service.DTO.Concrete;
using PMIS.DTO.Indicator.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.IndicatorValue.Info
{
    public class IndicatorValueTinyInfoDto : GenericSearchResponseDto
    {
        public async Task<IndicatorValueTinyInfoDto> extraMapFromBaseModel(PMIS.Models.IndicatorValue baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);
            this.shamsiDateTime = Helper.Convert.ConvertGregorianToShamsi(this.DateTime, "RRRR/MM/DD");
            return this;
        }
        public long Id { get; set; }

        public DateTime DateTime { get; set; }
        public string shamsiDateTime { get; set; }

        public decimal? Value { get; set; }
        public decimal? ValueCumulative { get; set; }
        public string? Description { get; set; }
        public int FkIndicatorId { get; set; }

        public int FkLkpValueTypeId { get; set; }

        public int FkLkpShiftId { get; set; }

        public virtual int? VrtLkpFormId { get; set; }=null;

        public virtual int? VrtLkpPeriodId { get; set; }=null;

    }
}
