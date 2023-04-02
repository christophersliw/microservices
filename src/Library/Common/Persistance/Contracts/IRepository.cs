namespace Common.Installers.Persistance.Contracts;

public interface IRepository<TEntity> where TEntity : class, IDataEntity
{
    IQueryable<TEntity> GetQuery();
}