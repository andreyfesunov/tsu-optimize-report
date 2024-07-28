using AutoMapper;
using BackendBase.Dto;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services;
using BackendBase.Models;

namespace BackendBase.Services;

public class LessonTypeService : ILessonTypeService
{
    protected readonly IMapper _mapper;
    protected readonly ILessonTypeRepository _repository;

    public LessonTypeService(ILessonTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<LessonType> AddEntity(LessonType entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<LessonTypeDto> GetById(Guid id)
    {
        return _mapper.Map<LessonTypeDto>(await _repository.GetById(id));
    }

    public async Task<ICollection<LessonTypeDto>> GetAll()
    {
        return (await _repository.GetAll()).Select(u => _mapper.Map<LessonTypeDto>(u)).ToList();
    }

    public async Task<LessonType> Update(LessonType entity)
    {
        return await _repository.UpdateEntity(entity);
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        return await _repository.DeleteById(entityId);
    }

    public async Task<Pagination<LessonTypeDto>> Search(SearchDto searchDto)
    {
        var result = await _repository.Search(searchDto);
        return new Pagination<LessonTypeDto>
        {
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalPages = result.TotalPages,
            Entities = result.Entities.Select(u => _mapper.Map<LessonTypeDto>(u)).ToList()
        };
    }

    public async Task<ICollection<LessonTypeDto>> GetAllForEvent(Guid stateUserId)
    {
        return (await _repository.GetAllForReport(stateUserId)).Select(x => _mapper.Map<LessonTypeDto>(x)).ToList();
    }
}
