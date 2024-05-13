using BackendBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackendBase.Models;
using BackendBase.Helpers.CRUD;
using BackendBase.Dto;
using AutoMapper;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonTypeController : CRUDControllerBase<LessonType, LessonTypeDto>
    {

        public LessonTypeController(ILessonTypeService service)
        : base(service)
        { }

    }
}
