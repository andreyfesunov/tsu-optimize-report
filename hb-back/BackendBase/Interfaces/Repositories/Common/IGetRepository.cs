namespace BackendBase.Interfaces.Repositories.Common;

public interface IGetRepository<TEntity>
{
    Task<TEntity> GetById(Guid id);
    Task<ICollection<TEntity>> GetAll();
}