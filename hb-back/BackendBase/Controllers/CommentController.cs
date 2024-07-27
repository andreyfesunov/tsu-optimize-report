using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.Comment;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICommentService _service;

    public CommentController(ICommentService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> Create(CommentCreateDto dto)
    {
        try
        {
            var comment = await _service.AddEntity(_mapper.Map<Comment>(dto));
            return Ok(_mapper.Map<CommentDto>(comment));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<CommentDto>> Update(CommentUpdateDto dto)
    {
        try
        {
            var comment = await _service.Update(dto);
            return Ok(_mapper.Map<CommentDto>(comment));
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
