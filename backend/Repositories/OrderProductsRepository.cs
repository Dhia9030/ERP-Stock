using StockManagement.Data;
using StockManagement.Models;

namespace StockManagement.Repositories;


public class OrderProductsRepository : BaseJointRepository<OrderProducts, Order, Product>, IOrderProductsRepository
{
    public OrderProductsRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsByOrderIdAsync(int orderId,bool asNoTracking = false, Func<IQueryable<OrderProducts>, IQueryable<OrderProducts>>? include = null)
    {
        return await GetSecondEntitiesByFirstIdAsync(orderId, nameof(OrderProducts.OrderId), nameof(OrderProducts.Product), asNoTracking,include);
    }

    public async Task<IEnumerable<Order>> GetOrdersByProductIdAsync(int productId, bool asNoTracking = false, Func<IQueryable<OrderProducts>, IQueryable<OrderProducts>>? include = null)
    {
        return await GetFirstEntitiesBySecondIdAsync(productId, nameof(OrderProducts.ProductId), nameof(OrderProducts.Order), asNoTracking,include);
    }
}
