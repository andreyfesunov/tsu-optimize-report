using BackendBase.Dto.Lesson;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.SecurityServices;
using BackendBase.Interfaces.Services;
using BackendBase.Models;

namespace BackendBase.Services;

public class LessonService : ILessonService
{
    private readonly ILessonRepository _repository;
    private readonly ILessonSecurityService _security;

    public LessonService(ILessonRepository repository, ILessonSecurityService security)
    {
        _repository = repository;
        _security = security;
    }

    public async Task<Lesson> AddEntity(Lesson entity)
    {
        await _security.validateCanUse(entity);
        await _security.validateCanCreate(entity);
        // ****

        return await _repository.AddEntity(entity);
    }

    public async Task<bool> DeleteById(Guid id)
    {
        var entity = await _repository.GetById(id);
        await _security.validateCanUse(entity);
        // ****

        return await _repository.Delete(entity);
    }

    public async Task<Lesson> Update(LessonUpdateDto dto)
    {
        var entity = await _repository.GetById(Guid.Parse(dto.Id));
        await _security.validateCanUse(entity);
        // ****

        entity.FactDate = dto.FactDate;
        entity.PlanDate = dto.PlanDate;

        return await _repository.UpdateEntity(entity);
    }
}
