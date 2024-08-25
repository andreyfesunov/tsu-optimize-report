using Tsu.IndividualPlan.WebApi.Dto.Lesson;
using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using Tsu.IndividualPlan.WebApi.Interfaces.SecurityServices;
using Tsu.IndividualPlan.WebApi.Interfaces.Services;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Services;

public class LessonService : ILessonService
{
    private readonly ILessonRepository _repository;
    private readonly ILessonSecurityService _security;

    public LessonService(ILessonRepository repository, ILessonSecurityService security)
    {
        _repository = repository;
        _security = security;
    }

    public async Task<Lesson> AddEntity(LessonCreateDto dto)
    {
        var entity = new Lesson(
            dto.EventId,
            dto.LessonTypeId,
            FactDate: dto.FactDate,
            PlanDate: dto.PlanDate
        );

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
        var entity = await _repository.GetById(dto.Id);
        await _security.validateCanUse(entity);
        // ****

        entity.FactDate = dto.FactDate;
        entity.PlanDate = dto.PlanDate;

        return await _repository.UpdateEntity(entity);
    }
}