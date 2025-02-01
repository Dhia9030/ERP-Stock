using backend.Dtos.TestDto;
using backend.Services.ServicesContract;
using Microsoft.EntityFrameworkCore;
using StockManagement.Models;
using StockManagement.Repositories;

namespace backend.Services.ConcreteServices;

public class StockMovementService : IStockMovementService
{
    private readonly IStockMovementRepository _stockMovementRepository;
    
    public StockMovementService(IStockMovementRepository stockMovementRepository)
    {
        _stockMovementRepository = stockMovementRepository;
    }
    
    public async Task<IEnumerable<StockMovementDto>> GetAllStockMovements()
    {
        var stockMovements = await _stockMovementRepository.GetAllAsync(
            include: include => include
                .Include(e => e.SourceProductBlock)
                    .ThenInclude(pb => pb.Product)
                        .ThenInclude(p => p.Category)
                .Include(e => e.DestinationProductBlock)
                .Include(sm => sm.StockMovementItems)
                    .ThenInclude(smi => smi.ProductItem)
                .Include(sm => sm.DestinationLocation)
                .Include(sm => sm.SourceLocation)
            
            );
        return stockMovements.Select(sm => new StockMovementDto
        {
            StockMovementId = sm.StockMovementId,
            Createdby = sm.CreatedBy,
            ProductName = sm.SourceProductBlock.Product.Name,
            CategoryName = sm.SourceProductBlock.Product.Category.Name,
            SourceLocationName = sm.SourceLocation.Name,
            DestinationLocationName = sm.DestinationLocation.Name,
            Quantity = sm.Quantity,
            MovementType = sm.MovementType,
            MovementDate = sm.MovementDate,
            SourceBlockId = sm.SourceProductBlock.ProductBlockId,
            DestinationBlockId = sm.DestinationProductBlock.ProductBlockId,
            productItemIds = sm.StockMovementItems.Select(smi => smi.ProductItemId).ToList()
        });
    }
    
    
    
    
    
    public async Task<ProductWithItemsDto> GetItemsForEachProductForSpecificBuyOrder(int OrderId, int ProductId)
    {
        var stockMovements = await _stockMovementRepository
            .FindAsync(m  => m.OrderId == OrderId 
                             && m.DestinationProductBlock.ProductId == ProductId
                                && m.MovementType == StockMovementStatus.Incoming,
            include: include => include
                .Include(e => e.DestinationProductBlock)
                    .ThenInclude(pb => pb.Product)
                .Include(sm => sm.StockMovementItems)
                    .ThenInclude(smi => smi.ProductItem)
                .Include(sm => sm.DestinationLocation)
                        .ThenInclude(sm => sm.Warehouse)
            );
        return stockMovements?.Select(sm => new ProductWithItemsDto
        {
            productname = sm?.DestinationProductBlock?.Product?.Name,
            items = sm?.StockMovementItems?.Select(smi => new ItemDto
            {
                ItemId = smi?.ProductItemId,
                ProductBlockId = sm?.DestinationProductBlockId,
                locationName = sm?.DestinationLocation?.Name,
                warehouseName = sm?.DestinationLocation?.Warehouse?.Name
            })?.ToList()
        }).FirstOrDefault();
        
    }
    
    
    
    
    

    public async Task<ProductWithItemsDto> GetItemsForEachProductForSpecificSellOrder(int OrderId, int ProductId)
    {
        
        var stockMovements = await _stockMovementRepository
            .FindAsync(m  => m.OrderId == OrderId 
                             && m.SourceProductBlock.ProductId == ProductId
                                && m.MovementType == StockMovementStatus.Outgoing,
            include: include => include
                .Include(e => e.SourceProductBlock)
                    .ThenInclude(pb => pb.Product)
                .Include(sm => sm.StockMovementItems)
                    .ThenInclude(smi => smi.ProductItem)
                .Include(sm => sm.SourceLocation)
                        .ThenInclude(sm => sm.Warehouse)
            );
        
        var itemsList = new List<ItemDto>();
        
        foreach (var Stockmv in stockMovements)
        {
            foreach (var item in Stockmv.StockMovementItems)
            {
                itemsList.Add(new ItemDto
                {
                    ItemId = item.ProductItemId,
                    ProductBlockId = Stockmv.SourceProductBlockId,
                    locationName = Stockmv.SourceLocation.Name,
                    warehouseName = Stockmv.SourceLocation.Warehouse.Name
                });
            }
            
        }
        var productName = stockMovements.FirstOrDefault()?.SourceProductBlock?.Product?.Name;
        
        return new ProductWithItemsDto
        {
            productname = productName,
            items = itemsList
        };
        
    }
    
    
}