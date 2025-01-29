using StockManagement.Models;

namespace backend.Services.ServicesContract;

public interface IGetProductService
{
    public Task<IEnumerable<Product>> GetAllProductsAsync();
    
    public Task<IEnumerable<Product>> GetAllClothingProduct();
    
    public Task<IEnumerable<Product>> GetAllFoodProduct();
    
    public Task<IEnumerable<Product>> GetAllElectronicProduct();
    
    
    
}