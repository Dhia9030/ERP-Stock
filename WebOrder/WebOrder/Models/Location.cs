using System;
using System.Collections.Generic;

namespace WebOrder.Models;

public partial class Location
{
    public int LocationId { get; set; }

    public string Name { get; set; } = null!;

    public int WarehouseId { get; set; }

    public virtual ICollection<ProductBlock> ProductBlocks { get; set; } = new List<ProductBlock>();

    public virtual ICollection<StockMovement> StockMovementDestinationLocations { get; set; } = new List<StockMovement>();

    public virtual ICollection<StockMovement> StockMovementSourceLocations { get; set; } = new List<StockMovement>();

    public virtual Warehouse Warehouse { get; set; } = null!;
}
