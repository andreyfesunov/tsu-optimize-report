using Microsoft.AspNetCore.Mvc;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Extensions.Entities;

namespace Tsu.IndividualPlan.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController(IJobService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<JobDto>> Create(string name)
    {
        try
        {
            var comment = await service.AddEntity(name);
            return Ok(comment.toDTO());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<JobDto>>> GetAll()
    {
        try
        {
            var result = await service.GetAll();
            return Ok(result.Select<Job, JobDto>(x => x.toDTO()).ToList());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}