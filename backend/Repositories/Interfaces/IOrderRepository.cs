namespace StockManagement.Repositories;
using StockManagement.Models;
using System.Linq.Expressions;


public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<BuyOrder>> GetAllBuyOrdersAsync();
    Task<IEnumerable<SellOrder>> GetAllSellOrdersAsync();
    
    Task<IEnumerable<BuyOrder>> FindBuyOrdersAsync(Expression<Func<BuyOrder, bool>> predicate);
    Task<IEnumerable<SellOrder>> FindSellOrdersAsync(Expression<Func<SellOrder, bool>> predicate);
    
}
