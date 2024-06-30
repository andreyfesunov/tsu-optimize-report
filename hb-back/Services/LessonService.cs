using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.Lesson;
using BackendBase.Helpers;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class LessonService : ILessonService
{
    protected readonly IMapper _mapper;
    protected MappingHelper<Lesson, LessonDto> _mappingHelper;
    protected IBaseRepository<Lesson> _repository;

    public LessonService(LessonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _mappingHelper = new MappingHelper<Lesson, LessonDto>(_mapper);
    }

    public async Task<Lesson> AddEntity(Lesson entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<LessonDto> GetById(Guid id)
    {
        return _mappingHelper.toDto(await _repository.GetById(id));
    }

    public async Task<ICollection<LessonDto>> GetAll()
    {
        return _mappingHelper.toDto(await _repository.GetAll());
    }

    public async Task<Lesson> Update(Lesson entity)
    {
        return await _repository.UpdateEntity(entity);
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        return await _repository.DeleteById(entityId);
    }

    public async Task<PaginationDto<LessonDto>> Search(SearchDto searchDto)
    {
        return _mappingHelper.paginationToDto(await _repository.Search(searchDto));
    }

    public async Task<LessonDto> Update(LessonUpdateDto dto)
    {
        var lesson = await _repository.GetById(Guid.Parse(dto.Id));

        lesson.FactDate = dto.FactDate;
        lesson.PlanDate = dto.PlanDate;

        lesson = await _repository.UpdateEntity(lesson);

        return _mapper.Map<LessonDto>(lesson);
    }
}