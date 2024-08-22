using BackendBase.Dto;
using BackendBase.Extensions.Entities;
using BackendBase.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivityController : ControllerBase
{
    private readonly IActivityService _service;

    public ActivityController(IActivityService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<ActivityDto>>> GetAll()
    {
        try
        {
            var result = await _service.GetAll();
            return Ok(result.Select(v => v.toDTO()));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
