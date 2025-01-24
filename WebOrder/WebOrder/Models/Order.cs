using System;
using System.Collections.Generic;

namespace WebOrder.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public double DiscountPercentage { get; set; }

    public int Status { get; set; }

    public int Type { get; set; }

    public int? SupplierId { get; set; }

    public int? ClientId { get; set; }

    public virtual Client? Client { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual ICollection<ProductItem> ProductItemPurchaseOrders { get; set; } = new List<ProductItem>();

    public virtual ICollection<ProductItem> ProductItemSaleOrders { get; set; } = new List<ProductItem>();

    public virtual Supplier? Supplier { get; set; }
}
