using BackendBase.Dto;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LessonTypeController : CRUDControllerBase<LessonType, LessonTypeDto>
{
    private readonly ILessonTypeService _service;

    public LessonTypeController(ILessonTypeService service)
        : base(service)
    {
        _service = service;
    }

    [HttpGet("{stateUserId:guid}")]
    public async Task<ActionResult<ICollection<LessonTypeDto>>> GetAllForEvent(Guid stateUserId)
    {
        try
        {
            var lessonTypes = await _service.GetAllForEvent(stateUserId);
            return Ok(lessonTypes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}