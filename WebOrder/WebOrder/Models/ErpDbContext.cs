using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebOrder.Models.ViewModels;

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

    public virtual DbSet<ProductBlock> ProductBlocks { get; set; }

    public virtual DbSet<ProductItem> ProductItems { get; set; }

    public virtual DbSet<ProductSupplier> ProductSuppliers { get; set; }

    public virtual DbSet<StockMovement> StockMovements { get; set; }

    public virtual DbSet<StockMovementItem> StockMovementItems { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("Server=localhost;Database=stockdb;User=root;Password=1234;", 
    ServerVersion.AutoDetect("Server=localhost;Database=STOCKDB;User=root;Password=1234;"));


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasIndex(e => e.WarehouseId, "IX_Locations_WarehouseId");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Warehouse).WithMany(p => p.Locations).HasForeignKey(d => d.WarehouseId);
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_Orders_ClientId");

            entity.HasIndex(e => e.SupplierId, "IX_Orders_SupplierId");

            entity.Property(e => e.OrderType).HasMaxLength(13);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 3)");

            entity.HasOne(d => d.Client).WithMany(p => p.Orders).HasForeignKey(d => d.ClientId);

            entity.HasOne(d => d.Supplier).WithMany(p => p.Orders).HasForeignKey(d => d.SupplierId);
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity.HasIndex(e => e.OrderId, "IX_OrderProducts_OrderId");

            entity.HasIndex(e => e.ProductId, "IX_OrderProducts_ProductId");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderProducts).HasForeignKey(d => d.OrderId);

            entity.HasOne(d => d.Product).WithMany(p => p.OrderProducts).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryId");

            entity.HasIndex(e => e.ManufacturerId, "IX_Products_ManufacturerId");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.EnergyClass).HasMaxLength(5);
            entity.Property(e => e.FabricType).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductType).HasMaxLength(13);

            entity.HasOne(d => d.Category).WithMany(p => p.Products).HasForeignKey(d => d.CategoryId);

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products).HasForeignKey(d => d.ManufacturerId);
        });

        modelBuilder.Entity<ProductBlock>(entity =>
        {
            entity.HasIndex(e => e.LocationId, "IX_ProductBlocks_LocationId");

            entity.HasIndex(e => e.ProductId, "IX_ProductBlocks_ProductId");

            entity.Property(e => e.ProductBlockType).HasMaxLength(21);

            entity.HasOne(d => d.Location).WithMany(p => p.ProductBlocks).HasForeignKey(d => d.LocationId);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductBlocks).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<ProductItem>(entity =>
        {
            entity.HasIndex(e => e.ProductBlockId, "IX_ProductItems_ProductBlockId");

            entity.HasOne(d => d.ProductBlock).WithMany(p => p.ProductItems).HasForeignKey(d => d.ProductBlockId);
        });

        modelBuilder.Entity<ProductSupplier>(entity =>
        {
            entity.ToTable("ProductSupplier");

            entity.HasIndex(e => e.ProductId, "IX_ProductSupplier_ProductId");

            entity.HasIndex(e => e.SupplierId, "IX_ProductSupplier_SupplierId");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductSuppliers).HasForeignKey(d => d.ProductId);

            entity.HasOne(d => d.Supplier).WithMany(p => p.ProductSuppliers).HasForeignKey(d => d.SupplierId);
        });

        modelBuilder.Entity<StockMovement>(entity =>
        {
            entity.HasIndex(e => e.DestinationLocationId, "IX_StockMovements_DestinationLocationId");

            entity.HasIndex(e => e.DestinationProductBlockId, "IX_StockMovements_DestinationProductBlockId");

            entity.HasIndex(e => e.OrderId, "IX_StockMovements_OrderId");

            entity.HasIndex(e => e.SourceLocationId, "IX_StockMovements_SourceLocationId");

            entity.HasIndex(e => e.SourceProductBlockId, "IX_StockMovements_SourceProductBlockId");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);

            entity.HasOne(d => d.DestinationLocation).WithMany(p => p.StockMovementDestinationLocations)
                .HasForeignKey(d => d.DestinationLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.DestinationProductBlock).WithMany(p => p.StockMovementDestinationProductBlocks)
                .HasForeignKey(d => d.DestinationProductBlockId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Order).WithMany(p => p.StockMovements).HasForeignKey(d => d.OrderId);

            entity.HasOne(d => d.SourceLocation).WithMany(p => p.StockMovementSourceLocations)
                .HasForeignKey(d => d.SourceLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SourceProductBlock).WithMany(p => p.StockMovementSourceProductBlocks)
                .HasForeignKey(d => d.SourceProductBlockId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<StockMovementItem>(entity =>
        {
            entity.HasIndex(e => e.ProductItemId, "IX_StockMovementItems_ProductItemId");

            entity.HasIndex(e => e.StockMovementId, "IX_StockMovementItems_StockMovementId");

            entity.HasOne(d => d.ProductItem).WithMany(p => p.StockMovementItems).HasForeignKey(d => d.ProductItemId);

            entity.HasOne(d => d.StockMovement).WithMany(p => p.StockMovementItems).HasForeignKey(d => d.StockMovementId);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.Property(e => e.Location).HasMaxLength(300);
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<WebOrder.Models.ViewModels.ConfirmationviewModel> ConfirmationviewModel { get; set; } = default!;


}
