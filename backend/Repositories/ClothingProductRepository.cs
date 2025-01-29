using StockManagement.Data;
using StockManagement.Models;
namespace StockManagement.Repositories;

public class ClothingProductRepository : Repository<ClothingProduct>, IClothingProductRepository
{
    public ClothingProductRepository(AppDbContext context) : base(context)
    {
    }
    
}