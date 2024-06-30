using AutoMapper;
using BackendBase.Dto;
using BackendBase.Helpers;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class LessonTypeService : ILessonTypeService
{
    protected readonly IMapper _mapper;
    protected MappingHelper<LessonType, LessonTypeDto> _mappingHelper;
    protected LessonTypeRepository _repository;

    public LessonTypeService(LessonTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _mappingHelper = new MappingHelper<LessonType, LessonTypeDto>(_mapper);
    }

    public async Task<LessonType> AddEntity(LessonType entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<LessonTypeDto> GetById(Guid id)
    {
        return _mappingHelper.toDto(await _repository.GetById(id));
    }

    public async Task<ICollection<LessonTypeDto>> GetAll()
    {
        return _mappingHelper.toDto(await _repository.GetAll());
    }

    public async Task<LessonType> Update(LessonType entity)
    {
        return await _repository.UpdateEntity(entity);
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        return await _repository.DeleteById(entityId);
    }

    public async Task<PaginationDto<LessonTypeDto>> Search(SearchDto searchDto)
    {
        return _mappingHelper.paginationToDto(await _repository.Search(searchDto));
    }

    public async Task<ICollection<LessonTypeDto>> GetAllForEvent(Guid stateUserId)
    {
        return (await _repository.GetAllForReport(stateUserId)).Select(x => _mapper.Map<LessonTypeDto>(x)).ToList();
    }
}