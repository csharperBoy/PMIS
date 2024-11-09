﻿using Generic.Base.Handler.Map;
using PMIS.DTO.Indicator.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.IndicatorValue.Info
{
    public class IndicatorValueTinyInfoDto
    {
        public async Task<IndicatorValueTinyInfoDto> extraMapFromBaseModel(PMIS.Models.IndicatorValue baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);
            return this;
        }
        public long Id { get; set; }


        public DateTime DateTime { get; set; }

        public decimal Value { get; set; }

        public string? Description { get; set; }

      
    }
}