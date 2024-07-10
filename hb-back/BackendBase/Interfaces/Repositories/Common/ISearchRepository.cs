using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces.Repositories.Common;

public interface ISearchRepository<TEntity>
{
    Task<Pagination<TEntity>> Search(SearchDto searchDto);
}