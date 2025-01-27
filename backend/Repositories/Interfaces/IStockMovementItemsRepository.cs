using StockManagement.Models;

namespace StockManagement.Repositories;

public interface IStockMovementItemsRepository : IRepository<StockMovementItems>
{
    Task<IEnumerable<ProductItem>> GetProductItemsByStockMovementIdAsync(int stockMovementId);
    Task<IEnumerable<StockMovement>> GetStockMovementsByProductItemIdAsync(int productItemId);
}