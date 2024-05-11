using BackendBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackendBase.Models;
using BackendBase.Helpers.CRUD;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonTypeController : CRUDControllerBase<LessonType>
    {
        public LessonTypeController(ILessonTypeService service)
        : base(service)
        {
        }
    }
}
