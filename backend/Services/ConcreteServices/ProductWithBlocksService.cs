using backend.Dtos.TestDto;
using backend.Services.ServicesContract;
using Microsoft.EntityFrameworkCore;
using StockManagement.Models;
using StockManagement.Repositories;

namespace backend.Services.ConcreteServices;

public class ProductWithBlocksService : IProductWithBlocksService
{
    private readonly IProductRepository _productRepository;
    private readonly IProductBlockRepository _productBlockRepository;
    
    public ProductWithBlocksService(IProductRepository productRepository, IProductBlockRepository productBlockRepository)
    {
        _productRepository = productRepository;
        _productBlockRepository = productBlockRepository;
    }
    
    public async Task<IEnumerable<ProductWithBlockDto>> GetAllProductWithBlocks()
    {
        var productsWithBlocks = await 
            _productRepository.GetAllAsync(
            include: include => include
                .Include(p => p.Category)
                .Include(e => e.ProductBlocks)
                    .ThenInclude(pb =>pb.Location)
                .Include(e => e.ProductBlocks)
                    .ThenInclude(pb =>pb.ProductItems)
            );
            return 
    productsWithBlocks.Select(p => new ProductWithBlockDto()
{
    ProductName = p.Name,
    CategoryName = p.Category?.Name,
    ProductBlocks = p.ProductBlocks?.Select(pb => new BlockDto
    {
        ProductBlockId = pb.ProductBlockId,
        LocationName = pb.Location?.Name,
        DiscountPercentage = pb.DiscountPercentage,
        quantity = pb.Quantity,
        Status = pb.Status,
        ExpirationDate = pb is FoodProductBlock fpb ? fpb.ExpirationDate : null,
        ProductItemIds = pb.ProductItems.ToDictionary(pi => pi.ProductItemId, pi => pi.Status)
        //ProductItemIds = pb.ProductItems?.ToDictionary(pi => pi.ProductItemId, pi => pi.Status) ?? new Dictionary<int, ProductItemStatus>()
    }).ToList() ?? new List<BlockDto>()
});
        
        
    }
    
}