﻿using BackendBase.Models;

namespace BackendBase.Interfaces.Repositories;

public interface IEventRepository
{
    Task<Event> AddEntity(Event entity);
    Task<Event> UpdateEntity(Event entity);
    Task<Event> GetById(Guid id);
    Task<bool> Delete(Event entity);
}
