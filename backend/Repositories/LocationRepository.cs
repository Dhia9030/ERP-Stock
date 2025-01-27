namespace StockManagement.Repositories;
using StockManagement.Models;
using StockManagement.Data;

public class LocationRepository : Repository<Location>, ILocationRepository
{
    public LocationRepository(AppDbContext context) : base(context)
    {
    }
}