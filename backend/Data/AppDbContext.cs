using Microsoft.EntityFrameworkCore;
using StockManagement.Models;

namespace StockManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet properties for each entity
        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClothingProduct> ClothingProducts { get; set; }
        public DbSet<ElectronicsProduct> ElectronicsProducts { get; set; }
        public DbSet<FoodProduct> FoodProducts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<ProductBlock> ProductBlocks { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<StockMovementPerItem> StockMovementPerItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure TPH (Table-per-Hierarchy) for Product inheritance
            modelBuilder.Entity<Product>()
                .HasDiscriminator<string>("ProductType")
                .HasValue<ClothingProduct>("Clothing")
                .HasValue<ElectronicsProduct>("Electronics")
                .HasValue<FoodProduct>("Food");

            // Configure relationships for ProductItem
            modelBuilder.Entity<ProductItem>()
                .HasOne(pi => pi.ProductBlock)
                .WithMany(pb => pb.ProductItems)
                .HasForeignKey(pi => pi.ProductBlockId);

            modelBuilder.Entity<ProductItem>()
                .HasOne(pi => pi.PurchaseOrder)
                .WithMany(o => o.PurchaseProductItems)
                .HasForeignKey(pi => pi.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductItem>()
                .HasOne(pi => pi.SaleOrder)
                .WithMany(o => o.SaleProductItems)
                .HasForeignKey(pi => pi.SaleOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure relationships for Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Supplier)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.StockMovement)
                .WithMany()
                .HasForeignKey(o => o.StockMovementId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure relationships for Location
            modelBuilder.Entity<Location>()
                .HasOne(l => l.Warehouse)
                .WithMany(w => w.Locations)
                .HasForeignKey(l => l.WarehouseId);

            // Configure relationships for Product
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Manufacturer)
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.ManufacturerId);

            // Configure relationships for ProductBlock
            modelBuilder.Entity<ProductBlock>()
                .HasOne(pb => pb.Product)
                .WithMany(p => p.ProductBlocks)
                .HasForeignKey(pb => pb.ProductId);

            modelBuilder.Entity<ProductBlock>()
                .HasOne(pb => pb.Location)
                .WithMany()
                .HasForeignKey(pb => pb.LocationId);

            // Configure relationships for StockMovement
            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.SourceLocation)
                .WithMany()
                .HasForeignKey(sm => sm.SourceLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.DestinationLocation)
                .WithMany()
                .HasForeignKey(sm => sm.DestinationLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.Order)
                .WithMany()
                .HasForeignKey(sm => sm.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure relationships for StockMovementPerItem
            modelBuilder.Entity<StockMovementPerItem>()
                .HasOne(smi => smi.Product)
                .WithMany()
                .HasForeignKey(smi => smi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockMovementPerItem>()
                .HasMany(smi => smi.ProductItems)
                .WithOne()
                .HasForeignKey(pi => pi.StockMovementPerItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}