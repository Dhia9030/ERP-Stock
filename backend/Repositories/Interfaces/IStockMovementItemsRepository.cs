using StockManagement.Models;

namespace StockManagement.Repositories;

public interface IStockMovementItemsRepository : IBaseJointRepository<StockMovementItems, StockMovement, ProductItem>
{
    public Task<IEnumerable<ProductItem>> GetProductItemsByStockMovementIdAsync(int stockMovementId,
        bool asNoTracking = false, bool includeStockMovement = false);

    public Task<IEnumerable<StockMovement>> GetStockMovementsByProductItemIdAsync(int productItemId,
        bool asNoTracking = false, bool includeProductItem = false);
}