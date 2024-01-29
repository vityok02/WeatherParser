using WeatherParser.Models.Interfaces;

namespace WeatherParser.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = _dbContext.Set<TEntity>().FindAsync(id, cancellationToken);
        return await entity;
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
