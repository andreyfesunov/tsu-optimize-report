using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Services
{
    public class EventTypeService : CRUDServiceBase<EventType, EventTypeDto>, IEventTypeService
    {
        public EventTypeService(EventTypeRepository repository)
        {
            _repository = repository;
        }
    }
}
