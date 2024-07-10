using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.Report;
using BackendBase.Helpers;
using BackendBase.Interfaces.Repositories.Common;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class StateUserService : IStateUserService
{
    protected readonly IMapper _mapper;
    protected MappingHelper<StateUser, ReportListDto> _mappingHelper;
    protected IBaseRepository<StateUser> _repository;

    public StateUserService(StateUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _mappingHelper = new MappingHelper<StateUser, ReportListDto>(_mapper);
    }

    public async Task<StateUser> AddEntity(StateUser entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<ReportListDto> GetById(Guid id)
    {
        return _mappingHelper.ToDto(await _repository.GetById(id));
    }

    public async Task<ICollection<ReportListDto>> GetAll()
    {
        return _mappingHelper.ToDto(await _repository.GetAll());
    }

    public async Task<StateUser> Update(StateUser entity)
    {
        return await _repository.UpdateEntity(entity);
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        return await _repository.DeleteById(entityId);
    }

    public async Task<Pagination<ReportListDto>> Search(SearchDto searchDto)
    {
        return _mappingHelper.ToDto(await _repository.Search(searchDto));
    }

    public async Task<ReportDetailDto> Detail(Guid id)
    {
        return _mapper.Map<ReportDetailDto>(await _repository.GetById(id));
    }
}