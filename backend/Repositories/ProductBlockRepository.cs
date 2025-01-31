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
    
    public async Task<IEnumerable<FoodProductBlock>> GetAllFoodProductBlockOrderedByExpirationDateAsync( int productId, Func<IQueryable<FoodProductBlock>, IQueryable<FoodProductBlock>>? include = null  )
    {  
        var querry = _dbSet.OfType<FoodProductBlock>();
        if (include != null)
        {
            querry = include(querry);
        }
        return await querry.Where(pb => pb.ProductId == productId && pb.Status == ProductBlockStatus.InStock).OrderBy(x => x.ExpirationDate).ToListAsync();
    }
    
    public async Task<IEnumerable<ProductBlock>> GetAllProductBlockAsync( int productId, Func<IQueryable<ProductBlock>, IQueryable<ProductBlock>>? include = null  )
    {  
        var querry = _dbSet.OfType<ProductBlock>();
        if (include != null)
        {
            querry = include(querry);
        }
        return await querry.Where(pb => pb.ProductId == productId && pb.Status == ProductBlockStatus.InStock).ToListAsync();
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