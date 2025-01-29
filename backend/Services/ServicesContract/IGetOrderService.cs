using Mysqlx.Crud;

namespace backend.Services.ServicesContract;

public interface IGetOrderService
{
    public IEnumerable<Order> GetAllOrders();
    public Order GetOrderDetail(int id);
    public void CancelOrder(int id);
    public void MarkOrderAsDelivered(int id);
    public void MarkOrderAsProcessing(int id);
}