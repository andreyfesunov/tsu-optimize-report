﻿using BackendBase.Dto;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
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

    [HttpPost("create")]
    public async Task<ActionResult<Work>> Create(Work entity)
    {
        try
        {
            var result = await _service.AddEntity(entity);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<WorkDto>> GetById(Guid Id)
    {
        try
        {
            var result = await _service.GetById(Id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getAll")]
    public async Task<ActionResult<ICollection<WorkDto>>> GetAll()
    {
        try
        {
            var result = await _service.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update")]
    public async Task<ActionResult<Work>> Update(Work entity)
    {
        try
        {
            var result = await _service.Update(entity);
            return Ok(result);
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

    [HttpPost("search")]
    public async Task<ActionResult<Pagination<WorkDto>>> Search([FromBody] SearchDto searchDto)
    {
        try
        {
            var result = await _service.Search(searchDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}