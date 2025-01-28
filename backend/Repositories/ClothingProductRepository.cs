using StockManagement.Data;

namespace StockManagement.Repositories;

public class ClothingProductRepository : Repository<ClothingProductRepository>, IClothingProductRepository
{
    public ClothingProductRepository(AppDbContext context) : base(context)
    {
    }
    
}