using StockManagement.Models;

namespace StockManagement.Repositories;

public interface IStockMovementItemsRepository : IBaseJointRepository<StockMovementItems, StockMovement, ProductItem>
{
    public Task<IEnumerable<ProductItem>> GetProductItemsByStockMovementIdAsync(int stockMovementId,
        bool asNoTracking = false, Func<IQueryable<StockMovementItems>, IQueryable<StockMovementItems>>? include = null);

    public Task<IEnumerable<StockMovement>> GetStockMovementsByProductItemIdAsync(int productItemId,
        bool asNoTracking = false, Func<IQueryable<StockMovementItems>, IQueryable<StockMovementItems>>? include = null);
}