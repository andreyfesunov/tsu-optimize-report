using BackendBase.Dto;
using BackendBase.Extensions.Entities;
using BackendBase.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController : ControllerBase
{
    private readonly IJobService _service;

    public JobController(IJobService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<JobDto>>> GetAll()
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
