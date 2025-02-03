using backend.Dtos.TestDto;
using backend.Services.ServicesContract;
using Microsoft.EntityFrameworkCore;
using StockManagement.Repositories;

namespace backend.Services.ConcreteServices;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly ILocationService _locationService;
    
    public WarehouseService(IWarehouseRepository warehouseRepository, ILocationService locationService)
    {
        _warehouseRepository = warehouseRepository;
        _locationService = locationService;
    }
    
    public async Task<WarehouseWithLocationDto> getWarehouseWithLocations(int warehouseId)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync("WarehouseId", warehouseId , q=> q.Include(w => w.Locations));
        var locations = new List<LocationWithBlockDto>();

        foreach (var location in warehouse.Locations)
        {
            var locationWithBlock = await _locationService.GetLocationWithBlocksById(location.LocationId);
            locations.Add(locationWithBlock);
            
        }
        
        return new WarehouseWithLocationDto
        {
            warehouseId = warehouse.WarehouseId,
            warehouseName = warehouse.Name,
            locations = locations
        };
    }
}