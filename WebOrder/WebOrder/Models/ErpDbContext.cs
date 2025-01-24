using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebOrder.Models;

public partial class ErpDbContext : DbContext
{
    public ErpDbContext()
    {
    }

    public ErpDbContext(DbContextOptions<ErpDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductItem> ProductItems { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-WebErp;Trusted_Connection=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasIndex(e => e.WarehouseId, "IX_Locations_WarehouseId");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Warehouse).WithMany(p => p.Locations).HasForeignKey(d => d.WarehouseId);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_Orders_ClientId");

            entity.HasIndex(e => e.SupplierId, "IX_Orders_SupplierId");

            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 3)");

            entity.HasOne(d => d.Client).WithMany(p => p.Orders).HasForeignKey(d => d.ClientId);

            entity.HasOne(d => d.Supplier).WithMany(p => p.Orders).HasForeignKey(d => d.SupplierId);
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId });

            entity.HasIndex(e => e.ProductId, "IX_OrderProducts_ProductId");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderProducts).HasForeignKey(d => d.OrderId);

            entity.HasOne(d => d.Product).WithMany(p => p.OrderProducts).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryId");

            entity.HasIndex(e => e.ManufacturerId, "IX_Products_ManufacturerId");

            entity.HasIndex(e => e.SupplierId, "IX_Products_SupplierId");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.EnergyClass).HasMaxLength(5);
            entity.Property(e => e.FabricType).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductType).HasMaxLength(13);

            entity.HasOne(d => d.Category).WithMany(p => p.Products).HasForeignKey(d => d.CategoryId);

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products).HasForeignKey(d => d.ManufacturerId);

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products).HasForeignKey(d => d.SupplierId);
        });

        modelBuilder.Entity<ProductItem>(entity =>
        {
            entity.HasKey(e => e.StockItemId);

            entity.HasIndex(e => e.ClientId, "IX_ProductItems_ClientId");

            entity.HasIndex(e => e.LocationId, "IX_ProductItems_LocationId");

            entity.HasIndex(e => e.ProductId, "IX_ProductItems_ProductId");

            entity.HasIndex(e => e.PurchaseOrderId, "IX_ProductItems_PurchaseOrderId");

            entity.HasIndex(e => e.SaleOrderId, "IX_ProductItems_SaleOrderId");

            entity.HasIndex(e => e.SupplierId, "IX_ProductItems_SupplierId");

            entity.Property(e => e.ProductItemType).HasMaxLength(13);

            entity.HasOne(d => d.Client).WithMany(p => p.ProductItems).HasForeignKey(d => d.ClientId);

            entity.HasOne(d => d.Location).WithMany(p => p.ProductItems).HasForeignKey(d => d.LocationId);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductItems).HasForeignKey(d => d.ProductId);

            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.ProductItemPurchaseOrders).HasForeignKey(d => d.PurchaseOrderId);

            entity.HasOne(d => d.SaleOrder).WithMany(p => p.ProductItemSaleOrders).HasForeignKey(d => d.SaleOrderId);

            entity.HasOne(d => d.Supplier).WithMany(p => p.ProductItems).HasForeignKey(d => d.SupplierId);
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.Property(e => e.Location).HasMaxLength(300);
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
