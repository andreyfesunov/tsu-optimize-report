using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces.Repositories;

public interface IDepartmentRepository
{
    Task<Pagination<Department>> Search(SearchDto searchDto);
}
