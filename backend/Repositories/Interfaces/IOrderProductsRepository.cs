using StockManagement.Models;

namespace StockManagement.Repositories;

public interface IOrderProductsRepository : IRepository<OrderProducts>
{
    
    Task<IEnumerable<Product>> GetProductsByOrderIdAsync(int orderId);
    Task<IEnumerable<Order>> GetOrdersByProductIdAsync(int productId);
}
    
    
