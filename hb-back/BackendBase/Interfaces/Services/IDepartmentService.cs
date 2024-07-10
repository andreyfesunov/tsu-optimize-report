using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface IDepartmentService
{
    Task<Department> AddEntity(Department entity);
    Task<DepartmentDto> GetById(Guid id);
    Task<ICollection<DepartmentDto>> GetAll();
    Task<Department> Update(Department entity);
    Task<bool> DeleteById(Guid entityId);
    Task<Pagination<DepartmentDto>> Search(SearchDto searchDto);
}