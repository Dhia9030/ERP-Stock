using StockManagement.Data;
using StockManagement.Models;

namespace StockManagement.Repositories;


public class OrderProductsRepository : BaseJointRepository<OrderProducts, Order, Product>, IOrderProductsRepository
{
    public OrderProductsRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsByOrderIdAsync(int orderId,bool asNoTracking = false, bool includeOrder = false)
    {
        return await GetSecondEntitiesByFirstIdAsync(orderId, nameof(OrderProducts.OrderId), nameof(OrderProducts.Product), asNoTracking,includeOrder);
    }

    public async Task<IEnumerable<Order>> GetOrdersByProductIdAsync(int productId, bool asNoTracking = false, bool includeProduct = false)
    {
        return await GetFirstEntitiesBySecondIdAsync(productId, nameof(OrderProducts.ProductId), nameof(OrderProducts.Order), asNoTracking,includeProduct);
    }
}
