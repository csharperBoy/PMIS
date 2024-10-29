using System;
using System.Collections.Generic;

namespace PMIS.Models;

public partial class Claim
{
    public int Id { get; set; }

    public int FkLkpClaimId { get; set; }

    public int FkUserId { get; set; }

    public int FkIndicatorId { get; set; }

    public string? Description { get; set; }

    public string? SystemInfo { get; set; }

    public bool? FlgLogicalDelete { get; set; }

    public virtual Indicator FkIndicator { get; set; } = null!;

    public virtual LookUpValue FkLkpClaim { get; set; } = null!;

    public virtual User FkUser { get; set; } = null!;
}
