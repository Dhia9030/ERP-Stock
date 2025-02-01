using StockManagement.Models;
using System.Linq.Expressions;

namespace StockManagement.Repositories;

public interface IProductBlockRepository : IRepository<ProductBlock>
{
    public Task<IEnumerable<FoodProductBlock>> GetAllFoodProductBlockOrderedByExpirationDateAsync(
        int productId ,Func<IQueryable<FoodProductBlock>, IQueryable<FoodProductBlock>>? include = null);

    public Task<IEnumerable<ProductBlock>> GetAllProductBlockForProductAsync( 
        int productId, Func<IQueryable<ProductBlock>, IQueryable<ProductBlock>>? include = null, bool asNoTracking = false  );

    public Task<ProductBlock?> FindProductBlockToTransfer(int productBlockId,
        Func<IQueryable<ProductBlock>, IQueryable<ProductBlock>>? include = null, bool asNoTracking = false);


}