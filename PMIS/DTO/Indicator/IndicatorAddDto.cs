﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.Indicator
{
    public class IndicatorAddRequestDto
    {
        // public int Id { get; set; }

        public string Code { get; set; } = null!;

        public string Title { get; set; } = null!;

        public int FkLkpFormId { get; set; }

        public int FkLkpManualityId { get; set; }

        public int FkLkpUnitId { get; set; }

        public int FkLkpPeriodId { get; set; }

        public int FkLkpMeasureId { get; set; }

        public int FkLkpDesirabilityId { get; set; }

        public string? Formula { get; set; }

        public string? Description { get; set; }

        public string? SystemInfo { get; set; }

        //public bool? FlgPhisycalDelete { get; set; }
    }
    public class IndicatorAddResponseDto
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

    }

}
