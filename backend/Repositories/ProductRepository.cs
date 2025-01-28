namespace StockManagement.Repositories;
using StockManagement.Models;
using StockManagement.Data;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

   
}