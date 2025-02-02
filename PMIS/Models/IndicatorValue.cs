using System;
using System.Collections.Generic;

namespace PMIS.Models;

public partial class IndicatorValue
{
    public long Id { get; set; }

    public int FkIndicatorId { get; set; }

    public int FkLkpValueTypeId { get; set; }

    public int FkLkpShiftId { get; set; }

    public DateTime DateTime { get; set; }

    public decimal Value { get; set; }

    public string? Description { get; set; }

    public string? SystemInfo { get; set; }

    public bool FlgLogicalDelete { get; set; }

    public decimal ValueCumulative { get; set; }

    public virtual Indicator FkIndicator { get; set; } = null!;

    public virtual LookUpValue FkLkpShift { get; set; } = null!;

    public virtual LookUpValue FkLkpValueType { get; set; } = null!;
}
