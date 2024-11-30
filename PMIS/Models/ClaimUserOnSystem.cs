using System;
using System.Collections.Generic;

namespace PMIS.Models;

public partial class ClaimUserOnSystem
{
    public int Id { get; set; }

    public int FkLkpClaimUserOnSystemId { get; set; }

    public int FkUserId { get; set; }

    public string? Description { get; set; }

    public string? SystemInfo { get; set; }

    public bool FlgLogicalDelete { get; set; }

    public virtual LookUpValue FkLkpClaimOnSystem { get; set; } = null!;

    public virtual User FkUser { get; set; } = null!;
}
