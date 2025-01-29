﻿namespace StockManagement.Repositories;
using StockManagement.Models;
using StockManagement.Data;

public class LocationRepository : Repository<Location>, ILocationRepository
{
    public LocationRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<Location?> GetFirstEmptyLocationForWarehouse(
        int warehouseId, 
        Func<IQueryable<Location>, IQueryable<Location>>? include = null, 
        bool asNoTracking = false)
    {
        var results = await FindAsync(x => x.WarehouseId == warehouseId && x.isEmpty == true, 
            query => (include != null ? include(query) : query).Take(1), 
            asNoTracking);
        return results.FirstOrDefault();  
    }

    public async Task<Location?> GetBuyerAreaLocation(
        string warehouseName,
        Func<IQueryable<Location>, IQueryable<Location>>? include = null,
        bool asNoTracking = false)
    {
        var results = await FindAsync(x => x.Name == $"{warehouseName} - Buyer Area" , 
            query => (include != null ? include(query) : query).Take(1), 
            asNoTracking);
        return results.FirstOrDefault();
    }
    
    public async Task<Location?> GetSupplierAreaLocation(
        string warehouseName,
        Func<IQueryable<Location>, IQueryable<Location>>? include = null,
        bool asNoTracking = false)
    {
        var results = await FindAsync(x => x.Name == $"{warehouseName} - Supplier Area" , 
            query => (include != null ? include(query) : query).Take(1), 
            asNoTracking);
        return results.FirstOrDefault();
    }

    public async Task<Location?> GetExpiredProductsAreaLocation(
        string warehouseName,
        Func<IQueryable<Location>, IQueryable<Location>>? include = null,
        bool asNoTracking = false)
    {
        var results = await FindAsync(x => x.Name == $"{warehouseName} - Expired Products Area" , 
            query => (include != null ? include(query) : query).Take(1), 
            asNoTracking);
        return results.FirstOrDefault();
    }
}