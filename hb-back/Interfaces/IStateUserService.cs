using BackendBase.Dto.Report;
using BackendBase.Helpers.CRUD;
using BackendBase.Models;

namespace BackendBase.Interfaces;

public interface IStateUserService : ICRUDServiceBase<StateUser, ReportListDto>
{
    Task<ReportDetailDto> Detail(Guid id);
}