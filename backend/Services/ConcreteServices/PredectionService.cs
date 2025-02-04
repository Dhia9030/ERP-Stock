using backend.Dtos.TestDto;
using backend.Services.ServicesContract;
using StockManagement.Models;
using StockManagement.Repositories;

namespace backend.Services.ConcreteServices;

public class PredectionService : IPredectionService

{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    
    public PredectionService(IOrderRepository orderRepository, IProductRepository productRepository
    , ICategoryRepository categoryRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }
    
    public async Task<PredectionDto> GetPredectionForProduct(int productId)
    {
        var product = await _productRepository.GetByIdAsync("ProductId",productId);
        var currentdate = DateTime.Now;
        var accumulatedQuantity = product.StockQuantity;
        
        var stockQuantity = new List<int>();
        
        for(var i = 0; i < 7; i++)
        {
            var date = currentdate.AddDays(i);
            var addedQuantity = _orderRepository.GetAddedQuantityForASpecificProductPerDayAsync(product.ProductId,date ).Result;
            var soldQuantity = _orderRepository.GetSoldQuantityForASpecificProductPerDayAsync(product.ProductId,date).Result;
            var result = addedQuantity - soldQuantity +accumulatedQuantity ;
            accumulatedQuantity = result;
            
            stockQuantity.Add(result);
            
        }
        
        return new PredectionDto
        {
            productName = product.Name,
            stockQuantity = stockQuantity
        };
    }
    
    public async Task<List<PredectionDto>> GetPredection()
    {
        var products = await _productRepository.GetAllAsync();
        var predections = new List<PredectionDto>();
        
        foreach (var product in products)
        {
            var predection = await GetPredectionForProduct(product.ProductId);
            predections.Add(predection);
        }
        
        return predections;
    } 
    
    public async Task<PredectionForAllCategoryDto> GetPredectionForCategory(Category category)
    {
        var products = await _productRepository.FindAsync(p => p.CategoryId==category.CategoryId);
        var predections = new List<PredectionDto>();
        
        foreach (var product in products)
        {
            var predection = await GetPredectionForProduct(product.ProductId);
            predections.Add(predection);
        }
        
        return new PredectionForAllCategoryDto
        {
            categoryName = category.Name,
            predectionDtos = predections
        };
    }
    
    public async Task<List<PredectionForAllCategoryDto>> GetPredectionForAllCategories()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var predections = new List<PredectionForAllCategoryDto>();
        
        foreach (var category in categories)
        {
            var predection = await GetPredectionForCategory(category);
            predections.Add(predection);
        }
        
        return predections;
    }
    
    
}