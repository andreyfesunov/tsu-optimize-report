using Microsoft.AspNetCore.Mvc;
using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Extensions.Entities;
using Tsu.IndividualPlan.WebApi.Interfaces.Services;

namespace Tsu.IndividualPlan.WebApi.Controllers;

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