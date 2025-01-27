using StockManagement.Models;
namespace StockManagement.Repositories;

public interface IProductSupplierRepository : IRepository<ProductSupplier>
{
    Task<IEnumerable<Product>> GetProductsBySupplierIdAsync(int supplierId);
    Task<IEnumerable<Supplier>> GetSuppliersByProductIdAsync(int productId);
}