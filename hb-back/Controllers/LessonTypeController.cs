using BackendBase.Dto;
using BackendBase.Helpers;
using BackendBase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackendBase.Models;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonTypeController : CRUDControllerBase<LessonType>
    {
        public LessonTypeController(ILessonTypeService serivce)
            : base(serivce)
        {
        }
    }
}
