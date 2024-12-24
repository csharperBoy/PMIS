using System;
using System.Collections.Generic;

namespace PMIS.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? FkParentId { get; set; }

    public string? CategoryCode { get; set; }

    public byte? OrderCategory { get; set; }

    public int FklkpTypeId { get; set; }

    public string? Description { get; set; }

    public string? SystemInfo { get; set; }

    public bool FlgLogicalDelete { get; set; }

    public virtual Category? FkParent { get; set; }

    public virtual LookUpValue FklkpType { get; set; } = null!;

    public virtual ICollection<IndicatorCategory> IndicatorCategories { get; set; } = new List<IndicatorCategory>();

    public virtual ICollection<Category> InverseFkParent { get; set; } = new List<Category>();
}
