using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using System.Reflection;

namespace StockManagement.Repositories
{
    public abstract class BaseJointRepository<TEntity, TFirstEntity, TSecondEntity> : Repository<TEntity>
        where TEntity : class
        where TFirstEntity : class
        where TSecondEntity : class
    {
        public BaseJointRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TSecondEntity>> GetSecondEntitiesByFirstIdAsync(
            int firstId,
            string firstEntityIdProperty,
            string secondEntityProperty,
            bool asNoTracking = false,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
        {
            ValidatePropertyNames(firstEntityIdProperty, secondEntityProperty);

            var query = _dbSet.AsQueryable();
            
            if (include != null)
            {
                query = include(query);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.Where(e => EF.Property<int>(e, firstEntityIdProperty) == firstId)
                .Select(e => EF.Property<TSecondEntity>(e, secondEntityProperty)).ToListAsync();
        }
        
        public async Task<IEnumerable<TFirstEntity>> GetFirstEntitiesBySecondIdAsync(
            int secondId,
            string secondEntityIdProperty,
            string firstEntityProperty,
            bool asNoTracking = false,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
        {
            ValidatePropertyNames(secondEntityIdProperty, firstEntityProperty);

            var query = _dbSet.AsQueryable();
            
            if (include != null)
            {
                query = include(query);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.Where(e => EF.Property<int>(e, secondEntityIdProperty) == secondId)
                .Select(e => EF.Property<TFirstEntity>(e, firstEntityProperty)).ToListAsync();
        }
        
        private void ValidatePropertyNames(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                var propertyInfo = typeof(TEntity).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (propertyInfo == null)
                {
                    throw new ArgumentException($"Property '{propertyName}' does not exist on entity '{typeof(TEntity).Name}'.");
                }
            }
        }
    }
}