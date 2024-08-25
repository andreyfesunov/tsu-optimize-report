using BackendBase.Dto;
using BackendBase.Dto.Lesson;
using BackendBase.Extensions.Entities;
using BackendBase.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LessonController : ControllerBase
{
    private readonly ILessonService _service;

    public LessonController(ILessonService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<LessonDto>> Create(LessonCreateDto dto)
    {
        try
        {
            var entity = await _service.AddEntity(dto);
            return Ok(entity.toDTO());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<LessonDto>> Update(LessonUpdateDto dto)
    {
        try
        {
            var entity = await _service.Update(dto);
            return Ok(entity.toDTO());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{entityId}")]
    public async Task<ActionResult<bool>> DeleteById(Guid entityId)
    {
        try
        {
            var result = await _service.DeleteById(entityId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
