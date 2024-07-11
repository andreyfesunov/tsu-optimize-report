using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;
using StudentHubBackend.Exceptions;

namespace BackendBase.Repositories;

public interface IWorkRepository
{
    Task<Work> AddEntity(Work entity);
    Task<Work> GetById(Guid id);
    Task<ICollection<Work>> GetAll();
    Task<Work> UpdateEntity(Work entity);
    Task<bool> DeleteById(Guid entityId);
    Task<Pagination<Work>> Search(SearchDto searchDto);
    IQueryable<Work> IncludeChildren(IQueryable<Work> query);
}