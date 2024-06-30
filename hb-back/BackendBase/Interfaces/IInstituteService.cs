using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces
{
    public interface IInstituteService
    {
        Task<Institute> AddEntity(Institute entity);
        Task<InstituteDto> GetById(Guid id);
        Task<ICollection<InstituteDto>> GetAll();
        Task<Institute> Update(Institute entity);
        Task<bool> DeleteById(Guid entityId);
        Task<PaginationDto<InstituteDto>> Search(SearchDto searchDto);
    }
}
