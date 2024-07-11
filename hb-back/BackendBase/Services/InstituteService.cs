using AutoMapper;
using BackendBase.Dto;
using BackendBase.Helpers;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class InstituteService : IInstituteService
{
    protected readonly IMapper _mapper;
    protected MappingHelper<Institute, InstituteDto> _mappingHelper;
    protected IInstituteRepository _repository;

    public InstituteService(IInstituteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _mappingHelper = new MappingHelper<Institute, InstituteDto>(_mapper);
    }

    public async Task<Institute> AddEntity(Institute entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<InstituteDto> GetById(Guid id)
    {
        return _mappingHelper.ToDto(await _repository.GetById(id));
    }

    public async Task<ICollection<InstituteDto>> GetAll()
    {
        return _mappingHelper.ToDto(await _repository.GetAll());
    }

    public async Task<Institute> Update(Institute entity)
    {
        return await _repository.UpdateEntity(entity);
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        return await _repository.DeleteById(entityId);
    }

    public async Task<Pagination<InstituteDto>> Search(SearchDto searchDto)
    {
        return _mappingHelper.ToDto(await _repository.Search(searchDto));
    }
}