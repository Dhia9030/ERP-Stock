using backend.Services.ServicesContract;
using Microsoft.EntityFrameworkCore;
using StockManagement.Models;
using StockManagement.Repositories;

namespace backend.Services.ConcreteServices;

public class MadeStockMovement : IMadeStockMovement
{
    private readonly IProductBlockRepository _productBlockRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IStockMovementRepository _stockMovementRepository;
    private readonly IProductItemRepository _productItemRepository;

    public MadeStockMovement(IProductBlockRepository productBlockRepository, ILocationRepository locationRepository, IStockMovementRepository stockMovementRepository , IProductItemRepository productItemRepository)
    {
        _productBlockRepository = productBlockRepository;
        _locationRepository = locationRepository;
        _stockMovementRepository = stockMovementRepository;
        _productItemRepository = productItemRepository;
    }

    public async Task<bool> TransferProductBlockAsync(int productBlockId, int newLocationId)
    {
        var productBlock = await _productBlockRepository.FindProductBlockToTransfer(productBlockId,asNoTracking : true);
        
        if ((productBlock == null) || (productBlock.LocationId == newLocationId ))
        {
            throw new ArgumentException("Product block not found or already in the same location.");
        }

        var newLocation = await _locationRepository.GetLocationByIdForTransfer(newLocationId, asNoTracking: true);
        
        if (newLocation == null)
        {
            throw new ArgumentException("New location not found.");
        }
        
        
        
        var stockMovement = new StockMovement
        {
            MovementType = StockMovementStatus.Transfer,
            CreatedBy = "System", // or get the current user
            MovementDate = DateTime.UtcNow,
            SourceProductBlockId = productBlockId,
            DestinationProductBlockId = productBlockId,
            SourceLocationId = productBlock.LocationId,
            DestinationLocationId = newLocationId,
            Quantity = productBlock.Quantity,
        };

        await _stockMovementRepository.AddAsync(stockMovement);
        
        var location = productBlock.Location;
        location.isEmpty = true;
        await _locationRepository.UpdateAsync(location);
        
        newLocation.isEmpty = false;
        await _locationRepository.UpdateAsync(newLocation);
        
        productBlock.LocationId = newLocationId;
        await _productBlockRepository.UpdateAsync(productBlock);
        return true;
    }
    
    
    public async Task<bool> MergeProductBlocksAsync(int sourceBlockId, int destinationBlockId)
    {
        var sourceBlock = await _productBlockRepository.GetByIdAsync(
            "ProductBlockId", sourceBlockId, q => q.Include(pb => pb.ProductItems)
                .Include(pb =>pb.Location)
                ,true);
        
        var destinationBlock = await _productBlockRepository.GetByIdAsync(
            "ProductBlockId", sourceBlockId, q => q.Include(pb => pb.ProductItems)
            ,true);
        

        if (sourceBlock == null || destinationBlock == null || sourceBlock.Status != ProductBlockStatus.InStock || destinationBlock.Status != ProductBlockStatus.InStock)
        {
            throw new ArgumentException("One or both product blocks not found or not in stock.");
        }

        if (sourceBlock.ProductId != destinationBlock.ProductId)
        {
            throw new InvalidOperationException("Cannot merge blocks of different products.");
        }

        if (sourceBlock is FoodProductBlock && ((FoodProductBlock)sourceBlock).ExpirationDate != ((FoodProductBlock)destinationBlock).ExpirationDate)
        {
            throw new InvalidOperationException("Cannot merge product blocks with different expiration dates.");
        }
        
        
        
        foreach (var item in sourceBlock.ProductItems.ToList())
        {
            item.ProductBlockId = destinationBlockId;
            await _productItemRepository.UpdateAsync(item);
            
        }
        
        destinationBlock.Quantity += sourceBlock.Quantity;
        await _productBlockRepository.UpdateAsync(destinationBlock);
        
        


        var stockMovement = new StockMovement
        {
            MovementType = StockMovementStatus.Merge,
            CreatedBy = "System", // or get the current user
            MovementDate = DateTime.UtcNow,
            SourceProductBlockId = sourceBlockId,
            DestinationProductBlockId = destinationBlockId,
            SourceLocationId = sourceBlock.LocationId,
            DestinationLocationId = destinationBlock.LocationId ?? 0,
            Quantity = sourceBlock.Quantity,
            Product = sourceBlock.Product
        };
        await _stockMovementRepository.AddAsync(stockMovement);

        var location = sourceBlock.Location;
        location.isEmpty = true;
        await _locationRepository.UpdateAsync(location);
        
        sourceBlock.Quantity = 0;
        sourceBlock.LocationId = null;
        sourceBlock.Status = ProductBlockStatus.MergedByOtherBlock;
        await _productBlockRepository.UpdateAsync(sourceBlock);

        return true;
    }
}