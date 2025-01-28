using StockManagement.Data;
using StockManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace StockManagement.Repositories;


public class ProductSupplierRepository : BaseJoinRepository<ProductSupplier, Supplier, Product>, IProductSupplierRepository
{
    public ProductSupplierRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsBySupplierIdAsync(int supplierId)
    {
        return await GetSecondEntitiesByFirstIdAsync(supplierId, nameof(ProductSupplier.SupplierId), nameof(ProductSupplier.Product));
    }

    public async Task<IEnumerable<Supplier>> GetSuppliersByProductIdAsync(int productId)
    {
        return await GetFirstEntitiesBySecondIdAsync(productId, nameof(ProductSupplier.ProductId), nameof(ProductSupplier.Supplier));
    }
}
