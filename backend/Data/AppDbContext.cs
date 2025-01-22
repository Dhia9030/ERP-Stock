using Microsoft.EntityFrameworkCore;

namespace StockManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets pour vos entités
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ClothingProduct> ClothingProducts { get; set; }
        public DbSet<ElectronicsProduct> ElectronicsProducts { get; set; }
        public DbSet<FoodProduct> FoodProducts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<CommandeProduit> CommandeProduits { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurations supplémentaires (facultatif)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Location)
                .WithMany(l => l.Products)
                .HasForeignKey(p => p.LocationId);

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
        }
    }
}
