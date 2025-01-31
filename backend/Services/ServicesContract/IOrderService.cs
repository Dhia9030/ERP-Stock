using StockManagement.Models;

namespace StockManagement.Services
{
    public interface IOrderService
    {
        public Task ExecuteBuyOrderAsync(int orderId);
        public Task ExecuteSellOrderAsync(int orderId);
    }
}