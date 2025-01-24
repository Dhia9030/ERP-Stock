using System;
using System.Collections.Generic;

namespace WebOrder.Models;

public partial class Warehouse
{
    public int WarehouseId { get; set; }

    public string Name { get; set; } = null!;

    public string? Location { get; set; }

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
