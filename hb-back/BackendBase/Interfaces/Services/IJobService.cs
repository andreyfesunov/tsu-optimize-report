﻿using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface IJobService
{
    Task<Job> AddEntity(Job entity);
    Task<JobDto> GetById(Guid id);
    Task<ICollection<JobDto>> GetAll();
    Task<Job> Update(Job entity);
    Task<bool> DeleteById(Guid entityId);
    Task<Pagination<JobDto>> Search(SearchDto searchDto);
}