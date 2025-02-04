using StockManagement.Models;

namespace backend.Services.ServicesContract;

public interface IOrderProductServices
{
    public Task<IEnumerable<Product>> getProductForSpecificOrder(int orderId);
}