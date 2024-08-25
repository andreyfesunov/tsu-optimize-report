using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Repositories;

public interface IDepartmentRepository
{
    Task<Pagination<Department>> Search(SearchDto searchDto);
}