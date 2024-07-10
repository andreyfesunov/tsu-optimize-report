using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface IWorkService
{
    Task<Work> AddEntity(Work entity);
    Task<WorkDto> GetById(Guid id);
    Task<ICollection<WorkDto>> GetAll();
    Task<Work> Update(Work entity);
    Task<bool> DeleteById(Guid entityId);
    Task<Pagination<WorkDto>> Search(SearchDto searchDto);
}