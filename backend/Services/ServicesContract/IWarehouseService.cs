using backend.Dtos.TestDto;

namespace backend.Services.ServicesContract;

public interface IWarehouseService
{
    public Task<WarehouseWithLocationDto> getWarehouseWithLocations(int warehouseId);
}