using backend.Dtos.TestDto;

namespace backend.Services.ServicesContract;

public interface IStockMovementService
{
    public Task<IEnumerable<StockMovementDto>> GetAllStockMovements();
    public Task<ProductWithItemsDto> GetItemsForEachProductForSpecificBuyOrder(int OrderId, int ProductId);
    
    public Task<ProductWithItemsDto> GetItemsForEachProductForSpecificSellOrder(int OrderId, int ProductId);
    
}