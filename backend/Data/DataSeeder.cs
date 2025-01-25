using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StockManagement.Models;
using System;
using System.Linq;

namespace StockManagement.Data
{
    public static class DataSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                // Vérifie si la base de données contient déjà des données
                if (context.Clients.Any() || context.Categories.Any() || context.Manufacturers.Any() || context.Suppliers.Any() || context.Products.Any())
                {
                    return;   // La base de données a déjà été peuplée
                }

                // Ajout des clients
                var clients = new[]
                {
                    new Client { LastName = "Doe", FirstName = "John", Email = "john.doe@example.com", Address = "123 Main St", PhoneNumber = "123-456-7890", RegistrationDate = DateTime.Now },
                    new Client { LastName = "Smith", FirstName = "Jane", Email = "jane.smith@example.com", Address = "456 Elm St", PhoneNumber = "987-654-3210", RegistrationDate = DateTime.Now }
                };
                context.Clients.AddRange(clients);

                // Ajout des catégories
                var categories = new[]
                {
                    new Category { Name = "Clothing", Description = "Apparel and garments" },
                    new Category { Name = "Electronics", Description = "Electronic devices and gadgets" },
                    new Category { Name = "Food", Description = "Edible items and groceries" }
                };
                context.Categories.AddRange(categories);

                // Ajout des fabricants
                var manufacturers = new[]
                {
                    new Manufacturer { Name = "Manufacturer A", Address = "789 Maple St", Email = "contact@manufacturerA.com", Phone = "555-123-4567" },
                    new Manufacturer { Name = "Manufacturer B", Address = "321 Oak St", Email = "contact@manufacturerB.com", Phone = "555-987-6543" }
                };
                context.Manufacturers.AddRange(manufacturers);

                // Ajout des fournisseurs
                var suppliers = new[]
                {
                    new Supplier { Name = "Supplier A", Email = "supplierA@example.com", Address = "654 Pine St", Phone = "555-654-3210", RegistrationDate = DateTime.Now },
                    new Supplier { Name = "Supplier B", Email = "supplierB@example.com", Address = "987 Birch St", Phone = "555-321-0987", RegistrationDate = DateTime.Now }
                };
                context.Suppliers.AddRange(suppliers);

                // Ajout des entrepôts
                var warehouses = new[]
                {
                    new Warehouse { Name = "Warehouse A", Location = "123 Main St" },
                    new Warehouse { Name = "Warehouse B", Location = "456 Elm St" }
                };
                context.Warehouses.AddRange(warehouses);

                // Ajout des emplacements
                var locations = new[]
                {
                    new Location { Name = "A1", WarehouseId = 1 },
                    new Location { Name = "B2", WarehouseId = 2 }
                };
                context.Locations.AddRange(locations);

                // Ajout des produits
                var products = new[]
                {
                    new ClothingProduct { Name = "T-Shirt", Price = 19.99m, StockQuantity = 100, CategoryId = 1, ManufacturerId = 1, FabricType = "Cotton", Size = ClothingProductStatus.M },
                    new ElectronicsProduct { Name = "Smartphone", Price = 299.99m, StockQuantity = 50, CategoryId = 2, ManufacturerId = 2, WarrantyYears = 2, EnergyClass = "A+" },
                    new FoodProduct { Name = "Apple", Price = 0.99m, StockQuantity = 200, CategoryId = 3, ManufacturerId = 1, StorageTemperature = 4 }
                };
                context.Products.AddRange(products);

                // Ajout des commandes
                var orders = new[]
                {
                    new Order { TotalAmount = 100.00m, DiscountPercentage = 10, Status = OrderStatus.Pending, Type = OrderType.Purchase, SupplierId = 1, ClientId = 1, MovementDate = DateTime.Now },
                    new Order { TotalAmount = 200.00m, DiscountPercentage = 5, Status = OrderStatus.Processing, Type = OrderType.Sales, SupplierId = 2, ClientId = 2, MovementDate = DateTime.Now }
                };
                context.Orders.AddRange(orders);

                // Ajout des blocs de produits
                var productBlocks = new[]
                {
                    new ProductBlock { ProductId = 1, LocationId = 1, Quantity = 50, Status = ProductBlockStatus.InStock },
                    new ProductBlock { ProductId = 2, LocationId = 2, Quantity = 20, Status = ProductBlockStatus.InStock }
                };
                context.ProductBlocks.AddRange(productBlocks);

                // Ajout des mouvements de stock
                var stockMovements = new[]
                {
                    new StockMovement { SourceLocationId = 1, DestinationLocationId = 2, MovementType = StockMovementStatus.Transfer, CreatedBy = "Admin", MovementDate = DateTime.Now, OrderId = 1 },
                    new StockMovement { SourceLocationId = 2, DestinationLocationId = 1, MovementType = StockMovementStatus.Transfer, CreatedBy = "Admin", MovementDate = DateTime.Now, OrderId = 2 }
                };
                context.StockMovements.AddRange(stockMovements);

                // Sauvegarde des données dans la base de données
                context.SaveChanges();
            }
        }
    }
}