using System;
using System.Collections.Generic;

namespace PMIS.Models;

public partial class ClaimOnSystem
{
    public int Id { get; set; }

    public int? FkLkpClaimOnSystemId { get; set; }

    public int? FkUserId { get; set; }

    public string? Description { get; set; }

    public string? SystemInfo { get; set; }

    public bool? FlgLogicalDelete { get; set; }

    public virtual LookUpValue? FkLkpClaimOnSystem { get; set; }

    public virtual User? FkUser { get; set; }
}
