using System;
using System.Collections.Generic;

namespace PMIS.Models;

public partial class LookUpValue
{
    public int Id { get; set; }

    public int FkLookUpId { get; set; }

    public string Value { get; set; } = null!;

    public string Display { get; set; } = null!;

    public int OrderNum { get; set; }

    public string? Description { get; set; }

    public string? SystemInfo { get; set; }

    public bool FlgLogicalDelete { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<ClaimUserOnIndicator> ClaimUserOnIndicators { get; set; } = new List<ClaimUserOnIndicator>();

    public virtual ICollection<ClaimUserOnSystem> ClaimUserOnSystems { get; set; } = new List<ClaimUserOnSystem>();

    public virtual LookUp FkLookUp { get; set; } = null!;

    public virtual ICollection<Indicator> IndicatorFkLkpDesirabilities { get; set; } = new List<Indicator>();

    public virtual ICollection<Indicator> IndicatorFkLkpForms { get; set; } = new List<Indicator>();

    public virtual ICollection<Indicator> IndicatorFkLkpManualities { get; set; } = new List<Indicator>();

    public virtual ICollection<Indicator> IndicatorFkLkpMeasures { get; set; } = new List<Indicator>();

    public virtual ICollection<Indicator> IndicatorFkLkpPeriods { get; set; } = new List<Indicator>();

    public virtual ICollection<Indicator> IndicatorFkLkpUnits { get; set; } = new List<Indicator>();

    public virtual ICollection<IndicatorValue> IndicatorValueFkLkpShifts { get; set; } = new List<IndicatorValue>();

    public virtual ICollection<IndicatorValue> IndicatorValueFkLkpValueTypes { get; set; } = new List<IndicatorValue>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
