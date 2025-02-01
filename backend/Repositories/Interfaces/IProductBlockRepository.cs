using StockManagement.Models;
using System.Linq.Expressions;

namespace StockManagement.Repositories;

public interface IProductBlockRepository : IRepository<ProductBlock>
{
    public Task<IEnumerable<FoodProductBlock>> GetAllFoodProductBlockOrderedByExpirationDateAsync(
        int productId ,Func<IQueryable<FoodProductBlock>, IQueryable<FoodProductBlock>>? include = null);

    public Task<IEnumerable<FoodProductBlock>> FindFoodProductBlockAsync(
        Expression<Func<FoodProductBlock, bool>> predicate,
        Func<IQueryable<FoodProductBlock>, IQueryable<FoodProductBlock>>? include = null);

    public Task<IEnumerable<ProductBlock>> GetAllProductBlockAsync(int productId,
        Func<IQueryable<ProductBlock>, IQueryable<ProductBlock>>? include = null);

}