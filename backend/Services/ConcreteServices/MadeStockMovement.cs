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
    private readonly IStockMovementItemsRepository _stockMovementItemRepository;
    private readonly IProductRepository _productRepository;

    public MadeStockMovement(IProductBlockRepository productBlockRepository, ILocationRepository locationRepository, IStockMovementRepository stockMovementRepository , 
        IProductItemRepository productItemRepository, IStockMovementItemsRepository stockMovementItemRepository,
        IProductRepository productRepository)
    {
        _productBlockRepository = productBlockRepository;
        _locationRepository = locationRepository;
        _stockMovementRepository = stockMovementRepository;
        _productItemRepository = productItemRepository;
        _stockMovementItemRepository = stockMovementItemRepository;
        _productRepository = productRepository;
        
    }

    public async Task<bool> TransferProductBlockAsync(int productBlockId, int newLocationId)
    {
        var productBlock = await _productBlockRepository.FindProductBlockToTransfer(productBlockId,q => q
            .Include(pb => pb.ProductItems)
            .Include(pb => pb.Location )
            ,  false);
        
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

        foreach (var productItem in productBlock.ProductItems)
        {
            /*
            productItem.ProductBlockId = productBlockId;
            await _productItemRepository.UpdateAsync(productItem);
            */
            var stockMovementItem = new StockMovementItems
            {
                ProductItemId = productItem.ProductItemId,
                StockMovementId = stockMovement.StockMovementId
            };

            await _stockMovementItemRepository.AddAsync(stockMovementItem);
            
        }
        
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
            "ProductBlockId", sourceBlockId, q => q
                .Include(pb => pb.ProductItems)
                .Include(pb =>pb.Location)
                );
        
        var destinationBlock = await _productBlockRepository.GetByIdAsync(
            "ProductBlockId", destinationBlockId, q => q.Include(pb => pb.ProductItems)
            );
        

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
        
        

        /*
        foreach (var item in sourceBlock.ProductItems.ToList())
        {
            item.ProductBlockId = destinationBlockId;
            await _productItemRepository.UpdateAsync(item);
            
        }
        */ //this code is false
        
        Console.WriteLine("*********************");
        Console.WriteLine(sourceBlock.Quantity);
        Console.WriteLine(destinationBlock.Quantity);
        
        
        destinationBlock.Quantity += sourceBlock.Quantity;
        Console.WriteLine(destinationBlock.Quantity);
        Console.WriteLine("*********************");
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
        
        var productItemList = sourceBlock.ProductItems.ToList();
        
        foreach (var productItem in productItemList)
        {
            productItem.ProductBlockId = destinationBlockId;
            await _productItemRepository.UpdateAsync(productItem);
            
            var stockMovementItem = new StockMovementItems
            {
                ProductItemId = productItem.ProductItemId,
                StockMovementId = stockMovement.StockMovementId
            };

            await _stockMovementItemRepository.AddAsync(stockMovementItem);
        }
        
        var location = sourceBlock.Location;
        location.isEmpty = true;
        await _locationRepository.UpdateAsync(location);
        
        sourceBlock.Quantity = 0;
        sourceBlock.LocationId = null;
        sourceBlock.Status = ProductBlockStatus.MergedByOtherBlock;
        await _productBlockRepository.UpdateAsync(sourceBlock);

        return true;
    }

    public async Task<bool> DeleteProductBlockAsync(int productBlockId)
    {
        var productBlock = await _productBlockRepository.GetByIdAsync("ProductBlockId", productBlockId, 
            q => q
                .Include(pb => pb.ProductItems)
                .Include(pb => pb.Location)
                .Include (pb => pb.Product)    
            );

        if (productBlock == null)
        {
            throw new Exception("Product block not found.");
        }
        
        if (productBlock.Status != ProductBlockStatus.InStock)
        {
            throw new InvalidOperationException("Cannot delete product block that is not in stock.");
        }

        if (productBlock is not FoodProductBlock )
        {
            throw new InvalidOperationException("Cannot delete product block that is not a food product.");
        }
        
        var foodProductBlock = (FoodProductBlock) productBlock;
        
        if(foodProductBlock.ExpirationDate > DateTime.UtcNow)
        {
            throw new InvalidOperationException("Cannot delete product block that is not expired.");
        }
        
        var stockMovement = new StockMovement
        {
            MovementType = StockMovementStatus.Delete,
            CreatedBy = "System", // or get the current user
            MovementDate = DateTime.UtcNow,
            SourceProductBlockId = productBlockId,
            DestinationProductBlockId = productBlockId,
            SourceLocationId = productBlock.LocationId ?? 3,
            DestinationLocationId = 3, // 3 is the id of the Expired location
            Quantity = productBlock.Quantity,
            Product = productBlock.Product,
        };
        await _stockMovementRepository.AddAsync(stockMovement);
        
        productBlock.Status = ProductBlockStatus.Expired;
        await _productBlockRepository.UpdateAsync(productBlock);
        
        if(productBlock.Quantity == 0)
        {
            return true;
        }

        
       foreach (var item in productBlock.ProductItems.ToList())
        {
            var stockMovementItem = new StockMovementItems
            {
                ProductItemId = item.ProductItemId,
                StockMovementId = stockMovement.StockMovementId
            };
            await _stockMovementItemRepository.AddAsync(stockMovementItem);
        
        }
       
        
        foreach (var item in productBlock.ProductItems.ToList())
        {
            
           
            item.Status = ProductItemStatus.Expired;
            await _productItemRepository.UpdateAsync(item);
        }
        
        var location = productBlock.Location;
        location.isEmpty = true;
        await _locationRepository.UpdateAsync(location);
        
        productBlock.LocationId = 3; // 3 is the id of the Expired location
        
        
        var product = productBlock.Product;
        product.StockQuantity -= productBlock.Quantity;
        await _productRepository.UpdateAsync(product);
        await _productBlockRepository.UpdateAsync(productBlock);

        return true;
    }
    
}