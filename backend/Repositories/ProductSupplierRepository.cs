using StockManagement.Data;
using StockManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace StockManagement.Repositories;


public class ProductSupplierRepository : BaseJointRepository<ProductSupplier, Supplier, Product>, IProductSupplierRepository
{
    public ProductSupplierRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsBySupplierIdAsync(int supplierId, bool asNoTracking = false, Func<IQueryable<ProductSupplier>, IQueryable<ProductSupplier>>? include = null)
    {
        return await GetSecondEntitiesByFirstIdAsync(supplierId, nameof(ProductSupplier.SupplierId), nameof(ProductSupplier.Product), asNoTracking, include);
    }

    public async Task<IEnumerable<Supplier>> GetSuppliersByProductIdAsync(int productId, bool asNoTracking = false, Func<IQueryable<ProductSupplier>, IQueryable<ProductSupplier>>? include = null)
    {
        return await GetFirstEntitiesBySecondIdAsync(productId, nameof(ProductSupplier.ProductId), nameof(ProductSupplier.Supplier), asNoTracking, include);
    }
}
