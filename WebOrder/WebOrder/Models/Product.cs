using System;
using System.Collections.Generic;

namespace WebOrder.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public int CategoryId { get; set; }

    public int ManufacturerId { get; set; }

    public int? SupplierId { get; set; }

    public string ProductType { get; set; } = null!;

    public string? FabricType { get; set; }

    public int? Size { get; set; }

    public int? WarrantyYears { get; set; }

    public string? EnergyClass { get; set; }

    public int? StorageTemperature { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual ICollection<ProductItem> ProductItems { get; set; } = new List<ProductItem>();

    public virtual Supplier? Supplier { get; set; }
}
