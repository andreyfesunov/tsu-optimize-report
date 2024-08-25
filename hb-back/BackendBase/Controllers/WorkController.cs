using BackendBase.Dto;
using BackendBase.Extensions.Entities;
using BackendBase.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkController : ControllerBase
{
    private readonly IWorkService _service;

    public WorkController(IWorkService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<WorkDto>>> GetAll()
    {
        try
        {
            var result = await _service.GetAll();
            return Ok(result.Select(x => x.toDTO()).ToList());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
