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
        return await _orderRepository.GetAllSellOrdersAsync( q => q.Include(e => e.Client));
    }

    public void CancelOrder(int orderId)
    {
        // Implementation here
    }

    public async Task<Order> GetOrderDetail(int orderId)
    {
        // Implementation here
        return await _orderRepository.GetByIdAsync("OrderId",orderId,q=>q.Include(e => e.Client).Include(o => o.OrderProducts).ThenInclude(op => op.Product));
    }

    public void MarkOrderAsDelivered(int orderId)
    {
        // Implementation here
    }

    public void MarkOrderAsProcessing(int orderId)
    {
        // Implementation here
    }

}