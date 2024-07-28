using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.Lesson;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LessonController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILessonService _service;

    public LessonController(ILessonService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<LessonDto>> Create(LessonCreateDto dto)
    {
        try
        {
            var entity = await _service.AddEntity(_mapper.Map<Lesson>(dto));
            return Ok(_mapper.Map<LessonDto>(entity));
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
            return Ok(_mapper.Map<LessonDto>(entity));
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
