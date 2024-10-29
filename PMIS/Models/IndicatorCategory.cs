using System;
using System.Collections.Generic;

namespace PMIS.Models;

public partial class IndicatorCategory
{
    public int Id { get; set; }

    public int FkIndicatorId { get; set; }

    public int FkLkpCategoryTypeId { get; set; }

    public int FkLkpCategoryMasterId { get; set; }

    public int FkLkpCategoryDetailId { get; set; }

    public int OrderNum { get; set; }

    public string? Description { get; set; }

    public string? SystemInfo { get; set; }

    public bool? FlgLogicalDelete { get; set; }

    public virtual Indicator FkIndicator { get; set; } = null!;

    public virtual LookUpValue FkLkpCategoryDetail { get; set; } = null!;

    public virtual LookUpValue FkLkpCategoryMaster { get; set; } = null!;

    public virtual LookUpValue FkLkpCategoryType { get; set; } = null!;
}
