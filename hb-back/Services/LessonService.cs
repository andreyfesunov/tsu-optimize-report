using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.Lesson;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class LessonService : CRUDServiceBase<Lesson, LessonDto>, ILessonService
{
    public LessonService(LessonRepository repository, IMapper mapper) : base(mapper)
    {
        _repository = repository;
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