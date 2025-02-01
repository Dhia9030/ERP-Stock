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
    
    public async Task<IEnumerable<ProductBlock>> GetAllProductBlockForProductAsync( int productId, Func<IQueryable<ProductBlock>, IQueryable<ProductBlock>>? include = null, bool asNoTracking = false  )
    {  
        return await FindAsync(pb => pb.ProductId == productId && pb.Status == ProductBlockStatus.InStock, include, asNoTracking);
    }
    
    public async Task<ProductBlock?> FindProductBlockToTransfer( int productBlockId, Func<IQueryable<ProductBlock>, IQueryable<ProductBlock>>? include = null, bool asNoTracking = false  )
    {  
        var results = await FindAsync(pb => pb.ProductBlockId == productBlockId && pb.Status == ProductBlockStatus.InStock, include, asNoTracking);
            
        return results.FirstOrDefault();
    }
    
}