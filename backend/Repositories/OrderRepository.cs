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
    
    public async Task<int> GetAddedQuantityForASpecificProductPerDayAsync(int productId, DateTime date)
    {
        var BuyOrdersInASpecificDay = await _dbSet.OfType<BuyOrder>()
            .Include(o => o.OrderProducts)
            .Where( o =>o.ExecutionDate.Day == date.Day && o.ExecutionDate.Month == date.Month && o.ExecutionDate.Year == date.Year).ToListAsync();
        if(BuyOrdersInASpecificDay == null)
        {
            return 0;
        }
        
        int AddedQuantity = 0;

        foreach (var order in BuyOrdersInASpecificDay)
        {
            foreach (var orderproduct in order.OrderProducts)
            {
                if(orderproduct.ProductId == productId)
                {
                    AddedQuantity += orderproduct.Quantity;
                }
            }
            
        }
        return AddedQuantity ;
    }
    
    public async Task<int> GetSoldQuantityForASpecificProductPerDayAsync(int productId, DateTime date)
    {
        var SellOrdersInASpecificDay = await _dbSet.OfType<SellOrder>()
            .Include(o => o.OrderProducts)
            .Where(o => o.ExecutionDate.Day == date.Day && o.ExecutionDate.Month == date.Month && o.ExecutionDate.Year == date.Year)
            .ToListAsync();
        if (SellOrdersInASpecificDay == null)
        {
            return 0;
        }

        int SoldQuantity = 0;

        foreach (var order in SellOrdersInASpecificDay)
        {
            foreach (var orderproduct in order.OrderProducts)
            {
                if (orderproduct.ProductId == productId)
                {
                    SoldQuantity += orderproduct.Quantity;
                }
            }
        }
        return SoldQuantity;
    }
    
}