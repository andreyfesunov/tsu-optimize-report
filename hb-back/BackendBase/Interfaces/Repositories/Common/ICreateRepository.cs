namespace BackendBase.Interfaces.Repositories.Common;

public interface ICreateRepository<TEntity>
{
    public Task<TEntity> AddEntity(TEntity entity);
}