namespace StockManagement.Repositories;
using StockManagement.Models;
using StockManagement.Data;

public class StockMovementRepository : Repository<StockMovement>, IStockMovementRepository
{
    public StockMovementRepository(AppDbContext context) : base(context)
    {
    }
    
}