using BackendBase.Dto;
using BackendBase.Dto.Report;
using BackendBase.Models;

namespace BackendBase.Interfaces;

public interface IStateUserService
{
    Task<StateUser> AddEntity(StateUser entity);
    Task<StateUserDto> GetById(Guid id);
    Task<ICollection<StateUserDto>> GetAll();
    Task<StateUser> Update(StateUser entity);
    Task<bool> DeleteById(Guid entityId);
    Task<PaginationDto<StateUserDto>> Search(SearchDto searchDto);
    Task<ReportDetailDto> Detail(Guid id);
}