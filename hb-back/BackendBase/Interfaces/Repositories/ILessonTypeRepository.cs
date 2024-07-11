using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;
using StudentHubBackend.Exceptions;

namespace BackendBase.Repositories;

public interface ILessonTypeRepository
{
    Task<LessonType> AddEntity(LessonType entity);
    Task<LessonType> GetById(Guid id);
    Task<ICollection<LessonType>> GetAll();
    Task<LessonType> UpdateEntity(LessonType entity);
    Task<bool> DeleteById(Guid entityId);
    Task<Pagination<LessonType>> Search(SearchDto searchDto);
    Task<bool> Save();
    IQueryable<LessonType> IncludeChildren(IQueryable<LessonType> query);
    Task<LessonType?> GetLessonTypeByName(string name);
    Task<ICollection<LessonType>> GetAllForReport(Guid stateUserId);
}