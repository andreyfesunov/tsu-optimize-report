using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;
using StudentHubBackend.Exceptions;

namespace BackendBase.Repositories
{
    public interface IActivityRepository
    {
        IQueryable<Activity> IncludeChildren(IQueryable<Activity> query);
        Task<Activity> AddEntity(Activity entity);
        Task<Activity> GetById(Guid id);
        Task<ICollection<Activity>> GetAll();
        Task<Activity> UpdateEntity(Activity entity);
        Task<bool> DeleteById(Guid entityId);
        Task<Pagination<Activity>> Search(SearchDto searchDto);
        Task<bool> Save();
    }
}
