using StockManagement.Data;
using StockManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace StockManagement.Repositories;


public class ProductSupplierRepository : BaseJointRepository<ProductSupplier, Supplier, Product>, IProductSupplierRepository
{
    public ProductSupplierRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsBySupplierIdAsync(int supplierId, bool asNoTracking = false, bool includeSuppliers = false)
    {
        return await GetSecondEntitiesByFirstIdAsync(supplierId, nameof(ProductSupplier.SupplierId), nameof(ProductSupplier.Product), asNoTracking, includeSuppliers);
    }

    public async Task<IEnumerable<Supplier>> GetSuppliersByProductIdAsync(int productId, bool asNoTracking = false, bool includeProducts = false)
    {
        return await GetFirstEntitiesBySecondIdAsync(productId, nameof(ProductSupplier.ProductId), nameof(ProductSupplier.Supplier), asNoTracking, includeProducts);
    }
}
