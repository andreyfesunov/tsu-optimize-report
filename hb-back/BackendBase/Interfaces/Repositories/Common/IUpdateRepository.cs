namespace BackendBase.Interfaces.Repositories.Common;

public interface IUpdateRepository<TEntity>
{
    public Task<TEntity> UpdateEntity(TEntity entity);
}
