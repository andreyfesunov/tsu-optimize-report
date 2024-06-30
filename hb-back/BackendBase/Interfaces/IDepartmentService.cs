using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces
{
    public interface IDepartmentService
    {
        Task<Department> AddEntity(Department entity);
        Task<DepartmentDto> GetById(Guid id);
        Task<ICollection<DepartmentDto>> GetAll();
        Task<Department> Update(Department entity);
        Task<bool> DeleteById(Guid entityId);
        Task<PaginationDto<DepartmentDto>> Search(SearchDto searchDto);
    }
}
