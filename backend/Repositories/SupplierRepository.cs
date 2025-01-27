namespace StockManagement.Repositories;

using StockManagement.Models;
using StockManagement.Data;

public class SupplierRepository : Repository<Supplier>, ISupplierRepository
{
    public SupplierRepository(AppDbContext context) : base(context)
    {
    }

}