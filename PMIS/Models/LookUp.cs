using System;
using System.Collections.Generic;

namespace PMIS.Models;

public partial class LookUp
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? SystemInfo { get; set; }

    public bool? FlgLogicalDelete { get; set; }

    public virtual ICollection<LookUpDestination> LookUpDestinations { get; set; } = new List<LookUpDestination>();

    public virtual ICollection<LookUpValue> LookUpValues { get; set; } = new List<LookUpValue>();
}
