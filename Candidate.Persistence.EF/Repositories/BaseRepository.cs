using Candidate.Application.Contracts.Persistence;

namespace Candidate.Persistence.EF.Repositories;

public class BaseRepository<T> : IAsyncRepository<T> where T : class
{
    public virtual Task<T> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public virtual Task<IReadOnlyList<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public virtual Task<T> AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual Task DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }
}