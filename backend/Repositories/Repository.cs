using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using StockManagement.Data;

namespace StockManagement.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        
        public async Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IQueryable<T>>? include = null, 
            bool asNoTracking = false)
        {
            var query = _dbSet.AsQueryable();
            
            if (include != null)
            {
                query = include(query);
            }
            
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }
        
        public async Task<T?> GetByIdAsync(
            string primaryKey,
            int id, 
            Func<IQueryable<T>, IQueryable<T>>? include = null, 
            bool asNoTracking = false)
        {
            var query = _dbSet.AsQueryable();
            
            if (include != null)
            {
                query = include(query);
            }
            
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, primaryKey) == id);
        }
        
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task<IEnumerable<T>> FindAsync(
            Expression<Func<T, bool>> predicate, 
            Func<IQueryable<T>, IQueryable<T>>? include = null, 
            bool asNoTracking = false)
        {
            var query = _dbSet.AsQueryable();
            
            if (include != null)
            {
                query = include(query);
            }
            
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.Where(predicate).ToListAsync();
        }
    }
}