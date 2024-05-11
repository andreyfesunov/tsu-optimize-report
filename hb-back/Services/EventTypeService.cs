using BackendBase.Data;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Services
{
    public class EventTypeService : CRUDServiceBase<EventType>, IEventTypeService
    {
        public EventTypeService(EventTypeRepository repository)
        {
            _repository = repository;
        }
    }
}
