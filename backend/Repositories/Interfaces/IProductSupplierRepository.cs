using StockManagement.Models;
namespace StockManagement.Repositories;

public interface IProductSupplierRepository : IBaseJointRepository<ProductSupplier, Supplier, Product>
{
    public Task<IEnumerable<Product>> GetProductsBySupplierIdAsync(int supplierId, bool asNoTracking = false,
        bool includeSuppliers = false);

    public Task<IEnumerable<Supplier>> GetSuppliersByProductIdAsync(int productId, bool asNoTracking = false,
        bool includeProducts = false);
}