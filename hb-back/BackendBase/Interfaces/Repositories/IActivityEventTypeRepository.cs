﻿using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;
using StudentHubBackend.Exceptions;

namespace BackendBase.Repositories;

public interface IActivityEventTypeRepository
{
    IQueryable<ActivityEventType> IncludeChildren(IQueryable<ActivityEventType> query);
    Task<bool> Validate(ActivityEventType model);
    Task<ActivityEventType> AddEntity(ActivityEventType entity);
    Task<ActivityEventType> GetById(Guid id);
    Task<ICollection<ActivityEventType>> GetAll();
    Task<ActivityEventType> UpdateEntity(ActivityEventType entity);
    Task<bool> DeleteById(Guid entityId);
    Task<Pagination<ActivityEventType>> Search(SearchDto searchDto);
    Task<bool> Save();
    Task<ICollection<ActivityEventType>> GetAll(Func<ActivityEventType, bool> predicate);
    Task<bool> DeleteBatch(IEnumerable<ActivityEventType> entities);
}