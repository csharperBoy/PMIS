using System;
using System.Collections.Generic;

namespace PMIS.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public int FkLkpWorkCalendarId { get; set; }

    public string? Phone { get; set; }

    public string? Description { get; set; }

    public string? SystemInfo { get; set; }

    public bool FlgLogicalDelete { get; set; }

    public virtual LookUpValue FkLkpWorkCalendar { get; set; } = null!;

    public virtual ICollection<ClaimUserOnIndicator> ClaimUserOnIndicators { get; set; } = new List<ClaimUserOnIndicator>();

    public virtual ICollection<ClaimUserOnSystem> ClaimUserOnSystems { get; set; } = new List<ClaimUserOnSystem>();
}
