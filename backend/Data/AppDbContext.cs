using Microsoft.EntityFrameworkCore;
using StockManagement.Models;

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
        public DbSet<Supplier> Fournisseurs { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<OrderProduct> CommandeProduits { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }
    }
}
