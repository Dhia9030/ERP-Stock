using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace StockManagement.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            bool asNoTracking = false);

        public Task<T?> GetByIdAsync(
            string primaryKey,
            int id,
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            bool asNoTracking = false);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        public Task<IEnumerable<T>> FindAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            bool asNoTracking = false);

        public Task<IDbContextTransaction> BeginTransactionAsync();
    }
}