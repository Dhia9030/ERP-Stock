using StockManagement.Models;
using StockManagement.Data;

namespace StockManagement.Repositories;

public class WarehouseRepository  : Repository<Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(AppDbContext context) : base(context)
    {
    }
    
}

