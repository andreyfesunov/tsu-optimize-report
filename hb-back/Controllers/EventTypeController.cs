using BackendBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackendBase.Models;
using BackendBase.Helpers.CRUD;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTypeController //: CRUDControllerBase<EventType>
    {
        public EventTypeController(IEventTypeService serivce)
            //: base(serivce)
        {
        }
    }
}
