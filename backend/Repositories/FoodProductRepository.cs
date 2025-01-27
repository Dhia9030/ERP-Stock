using StockManagement.Models;
using StockManagement.Data;

namespace StockManagement.Repositories;

public class FoodProductRepository : Repository<FoodProduct>, IFoodProductRepository
{
    public FoodProductRepository(AppDbContext context) : base(context)
    {
    }
    
}