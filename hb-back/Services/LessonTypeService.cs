using AutoMapper;
using BackendBase.Dto;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services
{
    public class LessonTypeService : CRUDServiceBase<LessonType, LessonTypeDto>, ILessonTypeService
    {
        public LessonTypeService(LessonTypeRepository repository, IMapper mapper) : base(mapper)
        {
            _repository = repository;
        }
    }
}
