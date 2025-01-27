using StockManagement.Models;
using System.Linq.Expressions;

namespace StockManagement.Repositories;

public interface IProductBlockRepository : IRepository<ProductBlock>
{
    Task<IEnumerable<FoodProductBlock>> GetAllFoodProductBlockAsync();
    Task<IEnumerable<FoodProductBlock>> FindFoodProductBlockAsync(Expression<Func<FoodProductBlock, bool>> predicate);
    
}