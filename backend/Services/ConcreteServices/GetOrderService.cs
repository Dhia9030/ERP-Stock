using backend.Services.ServicesContract;
using Microsoft.EntityFrameworkCore;
using StockManagement.Repositories;
using StockManagement.Models;

namespace backend.Services.ConcreteServices;

public class GetOrderService : IGetOrderService
{
    
    private readonly IOrderRepository _orderRepository;
    
    public GetOrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task<IEnumerable<Order>> GetAllSellOrders()
    {
        return await _orderRepository.GetAllSellOrdersAsync(q => q.Include(e => e.Client));
    }

    public async Task<IEnumerable<Order>> GetAllBuyOrders()
    {
        return await _orderRepository.GetAllBuyOrdersAsync(q => q.Include(e => e.Client));
    }
    public async Task CancelOrder(int orderId)
    {
        // Implementation here
    }

    public async Task<Order> GetOrderDetail(int orderId)
    {
        // Implementation here
        return await _orderRepository.GetByIdAsync("OrderId",orderId,q=>q.Include(e => e.Client).Include(e => e.Warehouse).Include(o => o.OrderProducts).ThenInclude(op => op.Product).ThenInclude(p => p.Category));
    }
    
    
    

    public async Task MarkOrderAsDelivered(int orderId)
    {
        // Implementation here
    }

    public async Task MarkOrderAsProcessing(int orderId)
    {
        // Implementation here
    }

}