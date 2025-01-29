using backend.Services.ServicesContract;
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
        return await _orderRepository.GetAllSellOrdersAsync();
    }

    public void CancelOrder(int orderId)
    {
        // Implementation here
    }

    public Order GetOrderDetail(int orderId)
    {
        // Implementation here
        throw new NotImplementedException();
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