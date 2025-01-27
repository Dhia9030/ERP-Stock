using System;
using System.Collections.Generic;

namespace WebOrder.Models;

public partial class ProductItem
{
    public int ProductItemId { get; set; }

    public int Status { get; set; }

    public int? ProductBlockId { get; set; }

    public virtual ProductBlock? ProductBlock { get; set; }

    public virtual ICollection<StockMovementItem> StockMovementItems { get; set; } = new List<StockMovementItem>();
}
