using backend.Services.ServicesContract;
using StockManagement.Repositories;
using StockManagement.Models;

namespace backend.Services.ConcreteServices;

public class GetOrderService : IGetOrderService
{
    
    private readonly IOrderRepository _orderRepository;
    public async Task<Order> GetAllOrders()
    {   
        return await _orderRepository.GetAllSellOrdersAsync;
    }
}