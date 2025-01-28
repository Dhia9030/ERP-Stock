using StockManagement.Data;
using StockManagement.Models;

namespace StockManagement.Repositories;

public class StockMovementItemsRepository : BaseJoinRepository<StockMovementItems, StockMovement, ProductItem>, IStockMovementItemsRepository
{
    public StockMovementItemsRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ProductItem>> GetProductItemsByStockMovementIdAsync(int stockMovementId)
    {
            return await GetSecondEntitiesByFirstIdAsync(stockMovementId, nameof(StockMovementItems.StockMovementId), nameof(StockMovementItems.ProductItem));
    }

    public async Task<IEnumerable<StockMovement>> GetStockMovementsByProductItemIdAsync(int productItemId)
    {
            return await GetFirstEntitiesBySecondIdAsync(productItemId, nameof(StockMovementItems.ProductItemId), nameof(StockMovementItems.StockMovement));
    }
}