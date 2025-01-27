using StockManagement.Models;
using StockManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace StockManagement.Repositories;

public class ProductBlockRepository : Repository<ProductBlock>,IProductBlockRepository
{
    public ProductBlockRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<FoodProductBlock>> GetAllFoodProductBlockAsync()
        {
            return await _dbSet
                .OfType<FoodProductBlock>() // Filter only ClothingProduct entities
                .ToListAsync();            // Execute the query asynchronously
        }
    
    
    public async Task<IEnumerable<FoodProductBlock>> FindFoodProductBlockAsync(Expression<Func<FoodProductBlock, bool>> predicate)
    {
        return await _dbSet
            .OfType<FoodProductBlock>() // Filter only FoodProduct entities
            .Where(predicate)      // Apply the predicate
            .ToListAsync();        // Execute the query asynchronously
    }

    
}