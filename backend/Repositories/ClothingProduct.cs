using StockManagement.Data;

namespace StockManagement.Repositories;

public class ClothingProduct : Repository<ClothingProduct>, IClothingProductRepository
{
    public ClothingProduct(AppDbContext context) : base(context)
    {
    }
    
}