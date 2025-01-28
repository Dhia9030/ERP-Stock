using StockManagement.Models;

namespace StockManagement.Repositories;

public interface IOrderProductsRepository : IBaseJointRepository<OrderProducts, Order, Product>
{

    public Task<IEnumerable<Product>> GetProductsByOrderIdAsync(int orderId, bool asNoTracking = false, bool includeOrder = false);

    public  Task<IEnumerable<Order>> GetOrdersByProductIdAsync(int productId, bool asNoTracking = false, bool includeProduct = false);
}
    
    
