using BackendBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackendBase.Models;
using BackendBase.Helpers.CRUD;
using BackendBase.Dto;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTypeController : CRUDControllerBase<EventType, EventTypeDto>
    {
        public EventTypeController(IEventTypeService service)
        : base(service)
        {
        }
    }
}
