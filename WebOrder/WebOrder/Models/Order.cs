using System;
using System.Collections.Generic;

namespace WebOrder.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ExecutionDate { get; set; }

    public string? DelayedDates { get; set; }

    public DateTime RealExecutionDate { get; set; }

    public double DiscountPercentage { get; set; }

    public int  Status { get; set; }

    public int Type { get; set; }
    
    public int WarehouseId { get; set; }

    public int? ClientId { get; set; }

    public string OrderType { get; set; } = null!;

    public int? SupplierId { get; set; }

    public virtual Client? Client { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();

    public virtual Supplier? Supplier { get; set; }
}

public enum OrderStatus
{
    Pending,
    Processing,
    Delivered,
    Cancelled,
    Delayed 
}

public enum OrderType
{
    Purchase,
    Sales
}