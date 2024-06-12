using AutoMapper;
using BackendBase.Dto;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class LessonTypeService : CRUDServiceBase<LessonType, LessonTypeDto>, ILessonTypeService
{
    private readonly LessonTypeRepository _repo;

    public LessonTypeService(LessonTypeRepository repository, IMapper mapper) : base(mapper)
    {
        _repo = repository;
    }

    public async Task<ICollection<LessonTypeDto>> GetAllForEvent(Guid stateUserId)
    {
        return (await _repo.GetAllForReport(stateUserId)).Select(x => _mapper.Map<LessonTypeDto>(x)).ToList();
    }
}