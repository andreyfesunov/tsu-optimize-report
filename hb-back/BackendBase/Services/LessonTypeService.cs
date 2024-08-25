using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services;
using BackendBase.Models;

namespace BackendBase.Services;

public class LessonTypeService : ILessonTypeService
{
    protected readonly ILessonTypeRepository _repository;

    public LessonTypeService(ILessonTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<LessonType>> GetAllForEvent(Guid stateUserId)
    {
        return await _repository.GetAllForReport(stateUserId);
    }
}
