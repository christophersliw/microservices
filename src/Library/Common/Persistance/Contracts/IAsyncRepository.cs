namespace Common.Installers.Persistance.Contracts;

public interface IAsyncRepository<TEntity> where TEntity : class, IDataEntity
{
    Task<TEntity> GetByIdAsync(Guid id);
    Task<List<TEntity>> GetAllAsync();
    Task<Guid> AddAsync(TEntity entity);
    Task<bool> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(Guid id);
}