using StockManagement.Models;
using StockManagement.Data;

namespace StockManagement.Repositories;

public class ManufacturerRepository : Repository<Manufacturer>, IManufacturerRepository
{
    public ManufacturerRepository(AppDbContext context) : base(context)
    {
        
    }
    
}