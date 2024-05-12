using BackendBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackendBase.Models;
using BackendBase.Helpers.CRUD;
using MathNet.Numerics.Statistics.Mcmc;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateUserController : CRUDControllerBase<StateUser>
    {
        private readonly IStateUserService _service;

        public StateUserController(IStateUserService service)
        : base(service)
        {
            _service = service;
        }

        [HttpGet("Include/{Id}")]
        public async Task<ActionResult<StateUser>> GetByIdInclude(Guid Id)
        {
            try
            {
                var result = await _service.GetByIdInclude(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
