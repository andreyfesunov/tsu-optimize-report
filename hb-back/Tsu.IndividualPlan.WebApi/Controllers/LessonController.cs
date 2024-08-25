using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Dto.Lesson;
using Tsu.IndividualPlan.WebApi.Extensions.Entities;
using Tsu.IndividualPlan.WebApi.Interfaces.Services;

namespace Tsu.IndividualPlan.WebApi.Controllers;

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