﻿using System;
using System.Collections.Generic;

namespace PMIS.Models;

public partial class Indicator
{
    public int Id { get; set; }

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

    public bool FlgLogicalDelete { get; set; }

    public virtual ICollection<ClaimUserOnIndicator> ClaimUserOnIndicators { get; set; } = new List<ClaimUserOnIndicator>();

    public virtual LookUpValue FkLkpDesirability { get; set; } = null!;

    public virtual LookUpValue FkLkpForm { get; set; } = null!;

    public virtual LookUpValue FkLkpManuality { get; set; } = null!;

    public virtual LookUpValue FkLkpMeasure { get; set; } = null!;

    public virtual LookUpValue FkLkpPeriod { get; set; } = null!;

    public virtual LookUpValue FkLkpUnit { get; set; } = null!;

    public virtual ICollection<IndicatorCategory> IndicatorCategories { get; set; } = new List<IndicatorCategory>();

    public virtual ICollection<IndicatorValue> IndicatorValues { get; set; } = new List<IndicatorValue>();
}
