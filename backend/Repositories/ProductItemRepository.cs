using StockManagement.Models;
using StockManagement.Data;

namespace StockManagement.Repositories;

public class ProductItemRepository : Repository<ProductItem>, IProductItemRepository
{
    public ProductItemRepository(AppDbContext context) : base(context)
    {
    }
    
}