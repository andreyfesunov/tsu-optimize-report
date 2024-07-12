using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.Lesson;
using BackendBase.Helpers;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class LessonService : ILessonService
{
    protected readonly IMapper _mapper;
    protected ILessonRepository _repository;

    public LessonService(ILessonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Lesson> AddEntity(Lesson entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<LessonDto> GetById(Guid id)
    {
        return _mapper.Map<LessonDto>(await _repository.GetById(id));
    }

    public async Task<ICollection<LessonDto>> GetAll()
    {
        return (await _repository.GetAll()).Select(u => _mapper.Map<LessonDto>(u)).ToList();
    }

    public async Task<Lesson> Update(Lesson entity)
    {
        return await _repository.UpdateEntity(entity);
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        return await _repository.DeleteById(entityId);
    }

    public async Task<Pagination<LessonDto>> Search(SearchDto searchDto)
    {
        var result = await _repository.Search(searchDto);
        return new Pagination<LessonDto>
        {
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalPages = result.TotalPages,
            Entities = result.Entities.Select(u => _mapper.Map<LessonDto>(u)).ToList()
        };
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