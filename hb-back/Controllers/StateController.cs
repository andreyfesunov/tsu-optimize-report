using BackendBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackendBase.Models;
using BackendBase.Helpers.CRUD;
using BackendBase.Dto;
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

        [HttpPost("setState")]
        public async Task<ActionResult<bool>> SetState(StateUserCreateDto entity)
        {
            try
            {
                var result = await _stateService.SetState(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
