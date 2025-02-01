using StockManagement.Models;

namespace backend.Services.ServicesContract;

public interface ILocationService
{
    public Task<IEnumerable<Location>> GetAllLocations();
    public Task<IEnumerable<Location>> GetFreeLocations();
}