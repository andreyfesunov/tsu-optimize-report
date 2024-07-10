using BackendBase.Dto;
using BackendBase.Dto.Report;
using BackendBase.Models;

namespace BackendBase.Interfaces;

public interface IStateUserService
{
    Task<StateUser> AddEntity(StateUser entity);
    Task<ReportListDto> GetById(Guid id);
    Task<ICollection<ReportListDto>> GetAll();
    Task<StateUser> Update(StateUser entity);
    Task<bool> DeleteById(Guid entityId);
    Task<PaginationDto<ReportListDto>> Search(SearchDto searchDto);
    Task<ReportDetailDto> Detail(Guid id);
}