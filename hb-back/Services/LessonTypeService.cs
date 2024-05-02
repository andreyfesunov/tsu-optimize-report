using BackendBase.Data;
using BackendBase.Helpers;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Services
{
    public class LessonTypeService : CRUDServiceBase<LessonType>, ILessonTypeService
    {
        public LessonTypeService(LessonTypeRepository repository)
        {
            _repository = repository;
        }
    }
}
