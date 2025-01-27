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
        public DbSet<ElectronicProduct> ElectronicsProducts { get; set; }
        public DbSet<FoodProduct> FoodProducts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBlock> ProductBlocks { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<StockMovementItems> StockMovementItems { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure TPH (Table-per-Hierarchy) for Product inheritance
            modelBuilder.Entity<Product>()
                .HasDiscriminator<string>("ProductType")
                .HasValue<ClothingProduct>("Clothing")
                .HasValue<ElectronicProduct>("Electronics")
                .HasValue<FoodProduct>("Food");
            
            // Configuration de l'héritage TPH pour Order, SellOrder, et BuyOrder
            modelBuilder.Entity<Order>()
                .HasDiscriminator<string>("OrderType")
                .HasValue<SellOrder>("SellOrder") // Valeur pour SellOrder
                .HasValue<BuyOrder>("BuyOrder"); // Valeur pour BuyOrder
            
            // Configure TPH (Table-per-Hierarchy) for ProductBlock inheritance
            modelBuilder.Entity<ProductBlock>()
                .HasDiscriminator<string>("ProductBlockType")
                .HasValue<ProductBlock>("ProductBlock")
                .HasValue<FoodProductBlock>("FoodProductBlock");
           

            // Configure relationships for ProductItem
            modelBuilder.Entity<ProductItem>()
                .HasOne(pi => pi.ProductBlock)
                .WithMany(pb => pb.ProductItems)
                .HasForeignKey(pi => pi.ProductBlockId);

            // Configure relationships for Order

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<BuyOrder>()
                .HasOne(bo => bo.Supplier)
                .WithMany(s => s.Orders)
                .HasForeignKey(bo => bo.SupplierId);

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
                .HasOne(sm => sm.Order)
                .WithMany(o => o.StockMovements)
                .HasForeignKey(sm => sm.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.SourceProductBlock)
                .WithMany()
                .HasForeignKey(sm => sm.SourceProductBlockId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.DestinationProductBlock)
                .WithMany()
                .HasForeignKey(sm => sm.DestinationProductBlockId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.Order)
                .WithMany(o => o.StockMovements)
                .HasForeignKey(sm => sm.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.DestinationLocation)
                .WithMany()
                .HasForeignKey(sm => sm.DestinationLocationId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.SourceLocation)
                .WithMany()
                .HasForeignKey(sm => sm.SourceLocationId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Configuration de la relation many-to-many entre Product et Order via OrderProducts
            modelBuilder.Entity<OrderProducts>()
                .HasKey(op => op.OrderProductId); // Clé primaire de la table de jointure

            modelBuilder.Entity<OrderProducts>()
                .HasOne(op => op.Product)
                .WithMany() // Un Product peut être dans plusieurs OrderProducts
                .HasForeignKey(op => op.ProductId);

            modelBuilder.Entity<OrderProducts>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts) // Un Order peut contenir plusieurs OrderProducts
                .HasForeignKey(op => op.OrderId);
            
            // Configuration de la relation many-to-many entre StockMovement et ProductItem via StockMovementItems
            modelBuilder.Entity<StockMovementItems>()
                .HasKey(smi => smi.StockMovementItemId); // Clé primaire de la table de jointure

            modelBuilder.Entity<StockMovementItems>()
                .HasOne(smi => smi.ProductItem)
                .WithMany() 
                .HasForeignKey(smi => smi.ProductItemId);

            modelBuilder.Entity<StockMovementItems>()
                .HasOne(smi => smi.StockMovement)
                .WithMany()
                .HasForeignKey(smi => smi.StockMovementId);
            
            // Configuration de la relation many-to-many entre Product et Supplier
            modelBuilder.Entity<ProductSupplier>()
                .HasKey(ps => ps.ProductSupplierId);

            modelBuilder.Entity<ProductSupplier>()
                .HasOne(ps => ps.Product)
                .WithMany()
                .HasForeignKey(ps => ps.ProductId);

            modelBuilder.Entity<ProductSupplier>()
                .HasOne(ps => ps.Supplier)
                .WithMany()
                .HasForeignKey(ps => ps.SupplierId);
        }
    }
}