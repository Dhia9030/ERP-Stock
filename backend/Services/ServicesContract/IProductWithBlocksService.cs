using backend.Services.ConcreteServices;
using backend.Dtos.TestDto;
namespace backend.Services.ServicesContract;

public interface IProductWithBlocksService
{
    public Task<IEnumerable<ProductWithBlockDto>> GetAllProductWithBlocks();
    
}