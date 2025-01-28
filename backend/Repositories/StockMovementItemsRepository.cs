using StockManagement.Data;
using StockManagement.Models;

namespace StockManagement.Repositories;

public class StockMovementItemsRepository : BaseJointRepository<StockMovementItems, StockMovement, ProductItem>, IStockMovementItemsRepository
{
    public StockMovementItemsRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ProductItem>> GetProductItemsByStockMovementIdAsync(int stockMovementId , bool asNoTracking = false,  Func<IQueryable<StockMovementItems>, IQueryable<StockMovementItems>>? include = null)
    {
            return await GetSecondEntitiesByFirstIdAsync(stockMovementId, nameof(StockMovementItems.StockMovementId), nameof(StockMovementItems.ProductItem), asNoTracking, include);
    }

    public async Task<IEnumerable<StockMovement>> GetStockMovementsByProductItemIdAsync(int productItemId, bool asNoTracking = false, Func<IQueryable<StockMovementItems>, IQueryable<StockMovementItems>>? include = null)
    {
            return await GetFirstEntitiesBySecondIdAsync(productItemId, nameof(StockMovementItems.ProductItemId), nameof(StockMovementItems.StockMovement), asNoTracking, include);
    }
}