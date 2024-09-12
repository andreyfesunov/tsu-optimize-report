using Tsu.IndividualPlan.Domain.Dto.Lesson;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Interfaces.SecurityServices;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Services;

public class LessonService(ILessonRepository repository, ILessonSecurityService security)
    : ILessonService
{
    public async Task<Lesson> AddEntity(LessonCreateDto dto)
    {
        var entity = new Lesson(
            dto.EventId,
            dto.LessonTypeId,
            FactDate: dto.FactDate,
            PlanDate: dto.PlanDate
        );

        await security.validateCanUse(entity);
        await security.validateCanCreate(entity);
        // ****

        return await repository.AddEntity(entity);
    }

    public async Task<bool> DeleteById(Guid id)
    {
        var entity = await repository.GetById(id);
        await security.validateCanUse(entity);
        // ****

        return await repository.Delete(entity);
    }

    public async Task<Lesson> Update(LessonUpdateDto dto)
    {
        var entity = await repository.GetById(dto.Id);
        await security.validateCanUse(entity);
        // ****

        entity.FactDate = dto.FactDate;
        entity.PlanDate = dto.PlanDate;

        return await repository.UpdateEntity(entity);
    }
}