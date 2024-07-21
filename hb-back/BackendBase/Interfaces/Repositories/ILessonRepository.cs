using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;
using StudentHubBackend.Exceptions;

namespace BackendBase.Repositories
{
    public interface ILessonRepository
    {
        Task<Lesson> AddEntity(Lesson entity);
        Task<Lesson> GetById(Guid id);
        Task<ICollection<Lesson>> GetAll();
        Task<Lesson> UpdateEntity(Lesson entity);
        Task<bool> DeleteById(Guid entityId);
        Task<Pagination<Lesson>> Search(SearchDto searchDto);
        Task<bool> Save();
        IQueryable<Lesson> IncludeChildren(IQueryable<Lesson> query);
    }
}
