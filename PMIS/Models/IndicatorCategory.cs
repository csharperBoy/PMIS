using System;
using System.Collections.Generic;

namespace PMIS.Models;

public partial class IndicatorCategory
{
    public int Id { get; set; }

    public int FkIndicatorId { get; set; }

    public int FkCategoryId { get; set; }

    public string? Description { get; set; }

    public string? SystemInfo { get; set; }

    public bool FlgLogicalDelete { get; set; }

    public virtual Category FkCategory { get; set; } = null!;

    public virtual Indicator FkIndicator { get; set; } = null!;
}
