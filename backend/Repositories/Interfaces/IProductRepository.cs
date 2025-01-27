namespace StockManagement.Repositories;
using StockManagement.Models;
using System.Linq.Expressions;

public interface IProductRepository : IRepository<Product>
{
    public Task<IEnumerable<ClothingProduct>> GetAllClothingProductsAsync(); 
    public Task<IEnumerable<ElectronicProduct>> GetAllElectronicProductsAsync();
    public Task<IEnumerable<FoodProduct>> GetAllFoodProductsAsync();
    
    
    public Task<IEnumerable<ClothingProduct>> FindClothingProductsAsync(Expression<Func<ClothingProduct, bool>> predicate);
    public Task<IEnumerable<ElectronicProduct>> FindElectronicProductsAsync(Expression<Func<ElectronicProduct, bool>> predicate);
    public Task<IEnumerable<FoodProduct>> FindFoodProductsAsync(Expression<Func<FoodProduct, bool>> predicate);
    
    
}