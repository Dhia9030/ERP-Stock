using System;
using System.Collections.Generic;

namespace WebOrder.Models;

public partial class ProductBlock
{
    public int ProductBlockId { get; set; }

    public int ProductId { get; set; }

    public int LocationId { get; set; }

    public double DiscountPercentage { get; set; }

    public int Quantity { get; set; }

    public int Status { get; set; }

    public string ProductBlockType { get; set; } = null!;

    public DateTime? ExpirationDate { get; set; }

    public virtual Location Location { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<ProductItem> ProductItems { get; set; } = new List<ProductItem>();

    public virtual ICollection<StockMovement> StockMovementDestinationProductBlocks { get; set; } = new List<StockMovement>();

    public virtual ICollection<StockMovement> StockMovementSourceProductBlocks { get; set; } = new List<StockMovement>();
}
