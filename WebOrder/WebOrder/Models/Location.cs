using System;
using System.Collections.Generic;

namespace WebOrder.Models;

public partial class Location
{
    public int LocationId { get; set; }

    public string Name { get; set; } = null!;

    public int WarehouseId { get; set; }

    public virtual ICollection<ProductItem> ProductItems { get; set; } = new List<ProductItem>();

    public virtual Warehouse Warehouse { get; set; } = null!;
}
