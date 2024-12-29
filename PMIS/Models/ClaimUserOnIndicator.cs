using System;
using System.Collections.Generic;

namespace PMIS.Models;

public partial class ClaimUserOnIndicator
{
    public int Id { get; set; }

    public int FkLkpClaimUserOnIndicatorId { get; set; }

    public int FkUserId { get; set; }

    public int FkIndicatorId { get; set; }

    public string? Description { get; set; }

    public string? SystemInfo { get; set; }

    public bool FlgLogicalDelete { get; set; }

    public virtual User FkUser { get; set; } = null!;

    public virtual LookUpValue FkLkpClaimUserOnIndicator { get; set; } = null!;

    public virtual Indicator FkIndicator { get; set; } = null!;
}
