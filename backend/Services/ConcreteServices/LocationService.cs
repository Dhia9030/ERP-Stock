using backend.Services.ServicesContract;
using Microsoft.EntityFrameworkCore;
using StockManagement.Models;
using StockManagement.Repositories;

namespace backend.Services.ConcreteServices;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    
    public LocationService(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }
    
    public async Task<IEnumerable<Location>> GetAllLocations()
    {
        var locations = await _locationRepository.GetAllAsync(q => q.Include(l => l.Warehouse));
        return locations;
    }

    public async Task<IEnumerable<Location>> GetFreeLocations()
    {
        var Locations = await GetAllLocations();
        return Locations.Where(l => l.isEmpty==true && l.LocationId > 3);
    }
    
}