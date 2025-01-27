using StockManagement.Data;
using StockManagement.Models;

namespace StockManagement.Repositories;


public class OrderProductsRepository : BaseJoinRepository<OrderProducts, Order, Product>, IOrderProductsRepository
{
    public OrderProductsRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsByOrderIdAsync(int orderId)
    {
        return await GetSecondEntitiesByFirstIdAsync(orderId, nameof(OrderProducts.OrderId), nameof(OrderProducts.Product));
    }

    public async Task<IEnumerable<Order>> GetOrdersByProductIdAsync(int productId)
    {
        return await GetFirstEntitiesBySecondIdAsync(productId, nameof(OrderProducts.ProductId), nameof(OrderProducts.Order));
    }
}
