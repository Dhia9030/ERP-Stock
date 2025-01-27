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
    
    public async Task<IEnumerable<BuyOrder>> GetAllBuyOrdersAsync()
    {
        return await _dbSet
            .OfType<BuyOrder>() 
            .ToListAsync();            
    }
    
    public async Task<IEnumerable<SellOrder>> GetAllSellOrdersAsync()
    {
        return await _dbSet
            .OfType<SellOrder>() 
            .ToListAsync();           
    }
    public async Task<IEnumerable<BuyOrder>> FindBuyOrdersAsync(Expression<Func<BuyOrder, bool>> predicate)
    {
        return await _dbSet
            .OfType<BuyOrder>() 
            .Where(predicate)      
            .ToListAsync();        
    }
    
    public async Task<IEnumerable<SellOrder>> FindSellOrdersAsync(Expression<Func<SellOrder, bool>> predicate)
    {
        return await _dbSet
            .OfType<SellOrder>() 
            .Where(predicate)      
            .ToListAsync();        
    }
    
}