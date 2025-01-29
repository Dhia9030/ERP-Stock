using backend.Services.ServicesContract;
using Microsoft.EntityFrameworkCore;
using StockManagement.Repositories;
using  StockManagement.Models;
namespace backend.Services.ConcreteServices;

public class GetProductService : IGetProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IClothingProductRepository _clothingProductRepository;
    private readonly IFoodProductRepository _foodProductRepository;
    private readonly IElectronicProductRepository _electronicProductRepository;
    
    public GetProductService(IProductRepository productRepository, IClothingProductRepository clothingProductRepository, IFoodProductRepository foodProductRepository, IElectronicProductRepository electronicProductRepository)
    {
        _productRepository = productRepository;
        _clothingProductRepository = clothingProductRepository;
        _foodProductRepository = foodProductRepository;
        _electronicProductRepository = electronicProductRepository;
    }
    
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync(q => q.Include(e => e.Category));
    }
    
    public async Task<IEnumerable<Product>> GetAllClothingProduct()
    {
        return await _clothingProductRepository.GetAllAsync(q => q.Include(e => e.Category));
    }
    
    public async Task<IEnumerable<Product>> GetAllFoodProduct()
    {
        return await _foodProductRepository.GetAllAsync(q => q.Include(e => e.Category));
    }
    
    public async Task<IEnumerable<Product>> GetAllElectronicProduct()
    {
        return await _electronicProductRepository.GetAllAsync(q => q.Include(e => e.Category));
    }
    
}