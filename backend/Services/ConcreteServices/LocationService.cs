using backend.Dtos.TestDto;
using backend.Services.ServicesContract;
using Microsoft.EntityFrameworkCore;
using StockManagement.Models;
using StockManagement.Repositories;

namespace backend.Services.ConcreteServices;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IProductBlockRepository _productBlockRepository;
    public LocationService(ILocationRepository locationRepository ,
        IProductBlockRepository productBlockRepository)
    {
        _locationRepository = locationRepository;
        _productBlockRepository = productBlockRepository;
    }
   
    
    public async Task<IEnumerable<Location>> GetAllLocations()
    {
        var locations = await _locationRepository.GetAllAsync(q => q.Include(l => l.Warehouse));
        return locations;
    }

    public async Task<IEnumerable<Location>> GetFreeLocations()
    {
        var Locations = await GetAllLocations();
        return Locations.Where(l => l.isEmpty==true && l.LocationId > 3);
    }

    public async Task<LocationWithBlockDto> GetLocationWithBlocksById(int id)
    {
        var location = await _locationRepository.GetByIdAsync("LocationId", id);
        var Blocks =   await _productBlockRepository
            .FindAsync(pb => pb.LocationId==id,
                p=> p.Include(pb => pb.Product)
                                                .Include(pb => pb.ProductItems));
        var block = Blocks.FirstOrDefault();
        
        return new LocationWithBlockDto
        {
            LocationId = location.LocationId,
            LocationName = location.Name,
            isEmpty = location.isEmpty,
            Block = block == null ? null : new BlockWithProductNameDto
            {
                ProductBlockId = block.ProductBlockId,
                productId = block.ProductId,
                productName = block.Product.Name,
                DiscountPercentage = block.DiscountPercentage,
                quantity = block.Quantity,
                Status = block.Status,
                ExpirationDate = null,//lina ne9iss champ date d expiration fil block
                ProductItemIds = block.ProductItems.ToDictionary(pi => pi.ProductItemId, pi => pi.Status)
            }
        };
        
    }
    
}