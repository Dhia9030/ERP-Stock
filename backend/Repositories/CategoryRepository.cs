namespace StockManagement.Repositories;
using StockManagement.Models;
using StockManagement.Data;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
    
}
