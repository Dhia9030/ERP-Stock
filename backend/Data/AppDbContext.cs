
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
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<ProductItemFood> ProductItemFoods { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure TPH (Table-per-Hierarchy) for Product inheritance
            modelBuilder.Entity<Product>()
                .HasDiscriminator<string>("ProductType")
                .HasValue<ClothingProduct>("Clothing")
                .HasValue<ElectronicsProduct>("Electronics")
                .HasValue<FoodProduct>("Food");

            // Configure TPH for ProductItem inheritance
            modelBuilder.Entity<ProductItem>()
                .HasDiscriminator<string>("ProductItemType")
                .HasValue<ProductItemFood>("Food");

            // Configure many-to-many relationship between Order and Product
            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany()
                .HasForeignKey(op => op.ProductId);

            // Configure relationships for ProductItem
            modelBuilder.Entity<ProductItem>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductItems)
                .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<ProductItem>()
                .HasOne(pi => pi.Location)
                .WithMany()
                .HasForeignKey(pi => pi.LocationId);

            modelBuilder.Entity<ProductItem>()
                .HasOne(pi => pi.Client)
                .WithMany()
                .HasForeignKey(pi => pi.ClientId);

            modelBuilder.Entity<ProductItem>()
                .HasOne(pi => pi.Supplier)
                .WithMany()
                .HasForeignKey(pi => pi.SupplierId);
          
            // Configure relationships for Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Supplier)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.SupplierId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Order>()
                .HasMany(o => o.PurchaseProductItems) // An Order can have many ProductItems
                .WithOne(pi => pi.PurchaseOrder) // A ProductItem belongs to one Order
                .HasForeignKey(pi => pi.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete; // Foreign key in ProductItem

            modelBuilder.Entity<Order>()
                .HasMany(o => o.SaleProductItems) // An Order can have many ProductItems
                .WithOne(pi => pi.SaleOrder) // A ProductItem belongs to one Order
                .HasForeignKey(pi => pi.SaleOrderId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete; // Foreign key in ProductItem

            
            // Configure relationships for Location
            modelBuilder.Entity<Location>()
                .HasOne(l => l.Warehouse)
                .WithMany()
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
            
           
        }
    }
}
