namespace StockManagement.Repositories;
using StockManagement.Models;
using StockManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    // Get all ClothingProducts
    public async Task<IEnumerable<ClothingProduct>> GetAllClothingProductsAsync()
    {
        return await _dbSet
            .OfType<ClothingProduct>() // Filter only ClothingProduct entities
            .ToListAsync();            // Execute the query asynchronously
    }

    // Get all ElectronicProducts
    public async Task<IEnumerable<ElectronicProduct>> GetAllElectronicProductsAsync()
    {
        return await _dbSet
            .OfType<ElectronicProduct>() // Filter only ElectronicProduct entities
            .ToListAsync();              // Execute the query asynchronously
    }

    // Get all FoodProducts
    public async Task<IEnumerable<FoodProduct>> GetAllFoodProductsAsync()
    {
        return await _dbSet
            .OfType<FoodProduct>() // Filter only FoodProduct entities
            .ToListAsync();        // Execute the query asynchronously
    }

    // Find ClothingProducts with a predicate
    public async Task<IEnumerable<ClothingProduct>> FindClothingProductsAsync(Expression<Func<ClothingProduct, bool>> predicate)
    {
        return await _dbSet
            .OfType<ClothingProduct>() // Filter only ClothingProduct entities
            .Where(predicate)          // Apply the predicate
            .ToListAsync();            // Execute the query asynchronously
    }

    // Find ElectronicProducts with a predicate
    public async Task<IEnumerable<ElectronicProduct>> FindElectronicProductsAsync(Expression<Func<ElectronicProduct, bool>> predicate)
    {
        return await _dbSet
            .OfType<ElectronicProduct>() // Filter only ElectronicProduct entities
            .Where(predicate)            // Apply the predicate
            .ToListAsync();              // Execute the query asynchronously
    }

    // Find FoodProducts with a predicate
    public async Task<IEnumerable<FoodProduct>> FindFoodProductsAsync(Expression<Func<FoodProduct, bool>> predicate)
    {
        return await _dbSet
            .OfType<FoodProduct>() // Filter only FoodProduct entities
            .Where(predicate)      // Apply the predicate
            .ToListAsync();        // Execute the query asynchronously
    }
}