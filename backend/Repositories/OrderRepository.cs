namespace StockManagement.Repositories;
using StockManagement.Models;
using StockManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;



public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<BuyOrder>> GetAllBuyOrdersAsync(Func<IQueryable<BuyOrder>, IQueryable<BuyOrder>>? include = null)
    {
        var querry = _dbSet.AsQueryable().OfType<BuyOrder>();
        if (include != null)
        {
            querry = include(querry);
        }
        return await querry.ToListAsync();            
    }
    
    public async Task<IEnumerable<SellOrder>> GetAllSellOrdersAsync(Func<IQueryable<SellOrder>, IQueryable<SellOrder>>? include = null)
    {
        var querry = _dbSet.AsQueryable().OfType<SellOrder>();
        if (include != null)
        {
            querry = include(querry);
        }
        return await querry.ToListAsync();    
    }
    public async Task<IEnumerable<BuyOrder>> FindBuyOrdersAsync(Expression<Func<BuyOrder, bool>> predicate , Func<IQueryable<BuyOrder>, IQueryable<BuyOrder>>? include = null)
    {
        var querry = _dbSet.AsQueryable().OfType<BuyOrder>();
        if (include != null)
        {
            querry = include(querry);
        }
        return await querry.Where(predicate).ToListAsync();
    }
    
    public async Task<IEnumerable<SellOrder>> FindSellOrdersAsync(Expression<Func<SellOrder, bool>> predicate ,Func<IQueryable<SellOrder>, IQueryable<SellOrder>>? include = null)
    {
        var querry = _dbSet.AsQueryable().OfType<SellOrder>();
        if (include != null)
        {
            querry = include(querry);
        }
        return await querry
            .Where(predicate)      
            .ToListAsync();        
    }
    
}