using StockManagement.Models;
namespace StockManagement.Repositories;

public interface IProductSupplierRepository : IBaseJointRepository<ProductSupplier, Supplier, Product>
{
    public Task<IEnumerable<Product>> GetProductsBySupplierIdAsync(int supplierId, bool asNoTracking = false,
        Func<IQueryable<ProductSupplier>, IQueryable<ProductSupplier>>? include = null);

    public Task<IEnumerable<Supplier>> GetSuppliersByProductIdAsync(int productId, bool asNoTracking = false,
        Func<IQueryable<ProductSupplier>, IQueryable<ProductSupplier>>? include = null);
}