using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;
using StudentHubBackend.Exceptions;

namespace BackendBase.Repositories
{
    public interface IInstituteRepository
    {
        Task<Institute> AddEntity(Institute entity);
        Task<Institute> GetById(Guid id);
        Task<ICollection<Institute>> GetAll();
        Task<Institute> UpdateEntity(Institute entity);
        Task<bool> DeleteById(Guid entityId);
        Task<Pagination<Institute>> Search(SearchDto searchDto);
        Task<bool> Save();
        IQueryable<Institute> IncludeChildren(IQueryable<Institute> query);
    }
}
