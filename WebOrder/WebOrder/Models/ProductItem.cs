using System;
using System.Collections.Generic;

namespace WebOrder.Models;

public partial class ProductItem
{
    public int StockItemId { get; set; }

    public int ProductId { get; set; }

    public int LocationId { get; set; }

    public int? ClientId { get; set; }

    public int? SupplierId { get; set; }

    public int Status { get; set; }

    public int? PurchaseOrderId { get; set; }

    public int? SaleOrderId { get; set; }

    public double DiscountPercentage { get; set; }

    public string ProductItemType { get; set; } = null!;

    public DateTime? ExpirationDate { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Location Location { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Order? PurchaseOrder { get; set; }

    public virtual Order? SaleOrder { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
