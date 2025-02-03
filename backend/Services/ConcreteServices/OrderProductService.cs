using backend.Services.ServicesContract;
using StockManagement.Models;
using StockManagement.Repositories;

namespace backend.Services.ConcreteServices;

public class OrderProductService : IOrderProductServices
{
    private readonly IOrderProductsRepository _orderProductsRepository;
    
    public OrderProductService(IOrderProductsRepository orderProductsRepository)
    {
        _orderProductsRepository = orderProductsRepository;
    }
    
    public async Task<IEnumerable<Product>> getProductForSpecificOrder(int orderId)
    {
        var productsWithItems = await _orderProductsRepository.GetProductsByOrderIdAsync(orderId);
        return productsWithItems;
    }
}