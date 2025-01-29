namespace StockManagement.Repositories;
using StockManagement.Models;

public interface ILocationRepository: IRepository<Location>
{
    public Task<Location?> GetFirstEmptyLocationForWarehouse(
        int warehouseId,
        Func<IQueryable<Location>, IQueryable<Location>>? include = null,
        bool asNoTracking = false);
    
    public Task<Location?> GetSupplierAreaLocation(
        string warehouseName,
        Func<IQueryable<Location>, IQueryable<Location>>? include = null,
        bool asNoTracking = false);
    public Task<Location?> GetBuyerAreaLocation(
        string warehouseName,
        Func<IQueryable<Location>, IQueryable<Location>>? include = null,
        bool asNoTracking = false);
    public Task<Location?> GetExpiredProductsAreaLocation(
        string warehouseName,
        Func<IQueryable<Location>, IQueryable<Location>>? include = null,
        bool asNoTracking = false);

}