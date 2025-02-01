using backend.Dtos.TestDto;

namespace backend.Services.ServicesContract;

public interface IStockMovementService
{
    public Task<IEnumerable<StockMovementDto>> GetAllStockMovements();
    
}