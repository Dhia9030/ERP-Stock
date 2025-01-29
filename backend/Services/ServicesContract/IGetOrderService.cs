
using StockManagement.Models;

namespace backend.Services.ServicesContract;

public interface IGetOrderService
{
    public Task<IEnumerable<Order>> GetAllSellOrders();
    public Order GetOrderDetail(int id);
    public void CancelOrder(int id);
    public void MarkOrderAsDelivered(int id);
    public void MarkOrderAsProcessing(int id);
}