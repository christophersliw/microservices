using Recruitment.Persistence.EF.Persistance;

namespace Recruitment.Persistence.EF.Repositories;

public class BaseRepository<T> : IAsyncRepository<T> where T : class
{
    public Task<T?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<T> AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }
}