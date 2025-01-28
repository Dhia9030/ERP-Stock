using StockManagement.Models;
using StockManagement.Data;


namespace StockManagement.Repositories;

public class ElectronicProductRepository  : Repository<ElectronicProduct>, IElectronicProductRepository
{
    public ElectronicProductRepository(AppDbContext context) : base(context)
    {
    }
    
}