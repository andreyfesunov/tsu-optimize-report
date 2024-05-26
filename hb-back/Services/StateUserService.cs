using AutoMapper;
using BackendBase.Dto.Report;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class StateUserService : CRUDServiceBase<StateUser, ReportListDto>, IStateUserService
{
    public StateUserService(StateUserRepository repository, IMapper mapper) : base(mapper)
    {
        _repository = repository;
    }

    public async Task<ReportDetailDto> Detail(Guid id)
    {
        return _mapper.Map<ReportDetailDto>(await _repository.GetById(id));
    }
}