using BackendBase.Dto;
using BackendBase.Extensions.Entities;
using BackendBase.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RecordController : ControllerBase
{
    private readonly IRecordService _recordService;

    public RecordController(IRecordService recordService)
    {
        _recordService = recordService;
    }

    [HttpGet("{stateUserId:guid}")]
    public async Task<ActionResult<RecordDto[]>> Get(Guid stateUserId)
    {
        try
        {
            var result = await _recordService.GetForReport(stateUserId);
            return Ok(result.Select(x => x.toDTO()).ToList());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
