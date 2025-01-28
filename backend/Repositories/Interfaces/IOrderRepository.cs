namespace StockManagement.Repositories;
using StockManagement.Models;
using System.Linq.Expressions;


public interface IOrderRepository : IRepository<Order>
{
    public Task<IEnumerable<BuyOrder>> GetAllBuyOrdersAsync(
        Func<IQueryable<BuyOrder>, IQueryable<BuyOrder>>? include = null);

    public Task<IEnumerable<SellOrder>> GetAllSellOrdersAsync(
        Func<IQueryable<SellOrder>, IQueryable<SellOrder>>? include = null);

    public Task<IEnumerable<BuyOrder>> FindBuyOrdersAsync(Expression<Func<BuyOrder, bool>> predicate,
        Func<IQueryable<BuyOrder>, IQueryable<BuyOrder>>? include = null);

    public Task<IEnumerable<SellOrder>> FindSellOrdersAsync(Expression<Func<SellOrder, bool>> predicate,
        Func<IQueryable<SellOrder>, IQueryable<SellOrder>>? include = null);

}
