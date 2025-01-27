using Microsoft.EntityFrameworkCore;
using StockManagement.Data;

namespace StockManagement.Repositories
{
    public abstract class BaseJoinRepository<TEntity, TFirstEntity, TSecondEntity> : Repository<TEntity>
        where TEntity : class
        where TFirstEntity : class
        where TSecondEntity : class
    {
        public BaseJoinRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TSecondEntity>> GetSecondEntitiesByFirstIdAsync(int firstId, string firstEntityIdProperty, string secondEntityProperty)
        {
            return await _dbSet
                .Include(e => EF.Property<TSecondEntity>(e, secondEntityProperty))
                .Where(e => EF.Property<int>(e, firstEntityIdProperty) == firstId)
                .Select(e => EF.Property<TSecondEntity>(e, secondEntityProperty))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<TFirstEntity>> GetFirstEntitiesBySecondIdAsync(int secondId, string secondEntityIdProperty, string firstEntityProperty)
        {
            return await _dbSet
                .Include(e => EF.Property<TFirstEntity>(e, firstEntityProperty))
                .Where(e => EF.Property<int>(e, secondEntityIdProperty) == secondId)
                .Select(e => EF.Property<TFirstEntity>(e, firstEntityProperty))
                .ToListAsync();
        }
    }
}