using Common.Installers.Persistance.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Common.Installers.Persistance;

public class BaseRepository<TEntity, TDbContext> : IAsyncRepository<TEntity>, IRepository<TEntity> where TEntity : class, IDataEntity where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;
    public BaseRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }
    
    public async Task<Guid> AddAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);

        await _dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
         
        var updated = await _dbContext.SaveChangesAsync();
        
        return updated > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var item = await GetByIdAsync(id);
        
        _dbContext.Set<TEntity>().Remove(item);
        
        var  deleted = await _dbContext.SaveChangesAsync();

        return deleted > 0;
    }

    public IQueryable<TEntity> GetQuery()
    {
        return _dbContext.Set<TEntity>();
    }
}