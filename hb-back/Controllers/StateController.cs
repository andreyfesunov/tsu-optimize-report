using BackendBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackendBase.Models;
using BackendBase.Helpers.CRUD;
using BackendBase.Dto;
using AutoMapper;
using BackendBase.Services;
using MathNet.Numerics.Statistics.Mcmc;
using BackendBase.Dto.CreateDto;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : CRUDControllerBase<State, StateDto>
    {
        private readonly IStateService _stateService;

        public StateController(IStateService service)
        : base(service)
        {
            _stateService = service;
        }

        [HttpPost("createWithDto")]
        public async Task<ActionResult<State>> Create(StateCreateDto entity)
        {
            try
            {
                var result = await _stateService.AddStateWithCreateDto(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
