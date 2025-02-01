using backend.Dtos.TestDto;
using backend.Services.ServicesContract;
using Microsoft.EntityFrameworkCore;
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
    
}