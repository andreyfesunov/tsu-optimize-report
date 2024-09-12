using Microsoft.AspNetCore.Mvc;
using Tsu.IndividualPlan.Domain.Dto.Comment;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Extensions.Entities;

namespace Tsu.IndividualPlan.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController(ICommentService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CommentDto>> Create(CommentCreateDto dto)
    {
        try
        {
            var comment = await service.AddEntity(dto);
            return Ok(comment.toDTO());
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
            var comment = await service.Update(dto);
            return Ok(comment.toDTO());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{entityId:guid}")]
    public async Task<ActionResult<bool>> DeleteById(Guid entityId)
    {
        try
        {
            var result = await service.DeleteById(entityId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}