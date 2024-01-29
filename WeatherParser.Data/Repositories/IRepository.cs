namespace WeatherParser.Data.Repositories;

public interface IRepository<TEntity>
{
    Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
