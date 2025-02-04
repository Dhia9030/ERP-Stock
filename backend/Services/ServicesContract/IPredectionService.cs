using backend.Dtos.TestDto;

namespace backend.Services.ServicesContract;

public interface IPredectionService
{
    public Task<List<PredectionDto>> GetPredection();
    public Task<PredectionDto> GetPredectionForProduct(int productId);
    
    
    public Task<List<PredectionForAllCategoryDto>> GetPredectionForAllCategories();
    
    
    
}