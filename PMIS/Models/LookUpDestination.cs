using System;
using System.Collections.Generic;

namespace PMIS.Models;

public partial class LookUpDestination
{
    public int Id { get; set; }

    public int FkLookUpId { get; set; }

    public string TableName { get; set; } = null!;

    public string ColumnName { get; set; } = null!;

    public string? Description { get; set; }

    public string? SystemInfo { get; set; }

    public bool FlgLogicalDelete { get; set; }

    public virtual LookUp FkLookUp { get; set; } = null!;
}
