namespace backend.Services.ServicesContract;

public interface IConfirmOrderService
{
    public Task ConfirmBuyOrderAsync(int orderId);
    public Task ConfirmSaleOrderAsync(int orderId);
}