namespace StockManagement.Repositories;

public interface IBaseJointRepository<TEntity, TFirstEntity, TSecondEntity> : IRepository<TEntity>
    where TEntity : class
    where TFirstEntity : class
    where TSecondEntity : class
{
    public Task<IEnumerable<TSecondEntity>> GetSecondEntitiesByFirstIdAsync(int firstId,
        string firstEntityIdProperty, string secondEntityProperty, bool asNoTracking = false,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null);

    public  Task<IEnumerable<TFirstEntity>> GetFirstEntitiesBySecondIdAsync(int secondId,
        string secondEntityIdProperty, string firstEntityProperty, bool asNoTracking = false,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null);

}