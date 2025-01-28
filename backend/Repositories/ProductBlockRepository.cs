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
    
    public async Task<IEnumerable<FoodProductBlock>> GetAllFoodProductBlockAsync( Func<IQueryable<FoodProductBlock>, IQueryable<FoodProductBlock>>? include = null)
    {
        var querry = _dbSet.OfType<FoodProductBlock>();
        if (include != null)
        {
            querry = include(querry);
        }
        return await querry.ToListAsync();            
        }
    
    
    public async Task<IEnumerable<FoodProductBlock>> FindFoodProductBlockAsync(Expression<Func<FoodProductBlock, bool>> predicate,Func<IQueryable<FoodProductBlock>, IQueryable<FoodProductBlock>>? include = null)
    {
        var querry = _dbSet.OfType<FoodProductBlock>();
        if (include != null)
        {
            querry = include(querry);
        }
        return await querry.Where(predicate).ToListAsync();
    }

    
}