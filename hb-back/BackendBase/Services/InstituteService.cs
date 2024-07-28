using AutoMapper;
using BackendBase.Dto;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services;
using BackendBase.Models;

namespace BackendBase.Services;

public class InstituteService : IInstituteService
{
    protected readonly IMapper _mapper;
    protected IInstituteRepository _repository;

    public InstituteService(IInstituteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Institute> AddEntity(Institute entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<InstituteDto> GetById(Guid id)
    {
        return _mapper.Map<InstituteDto>(await _repository.GetById(id));
    }

    public async Task<ICollection<InstituteDto>> GetAll()
    {
        return (await _repository.GetAll()).Select(u => _mapper.Map<InstituteDto>(u)).ToList();
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
        var result = await _repository.Search(searchDto);
        return new Pagination<InstituteDto>
        {
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalPages = result.TotalPages,
            Entities = result.Entities.Select(u => _mapper.Map<InstituteDto>(u)).ToList()
        };
    }
}
