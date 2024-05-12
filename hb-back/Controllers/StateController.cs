using BackendBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackendBase.Models;
using BackendBase.Helpers.CRUD;
using BackendBase.Dto;
using AutoMapper;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : CRUDControllerBase<State>
    {
        private readonly IMapper _mapper;
        private readonly IStateService _stateService;

        public StateController(IStateService service, IMapper mapper)
        : base(service)
        {
            _stateService = service;
            _mapper = mapper;
        }

        [HttpPost("search")]
        public async Task<ActionResult<PaginationDto<StateDto>>> Search(SearchDto searchDto)
        {
            try
            {
                var result = await _stateService.Search(searchDto);
                return Ok(new PaginationDto<StateDto>
                {
                    PageNumber = result.PageNumber,
                    PageSize = result.PageSize,
                    TotalPages = result.TotalPages,
                    Entities = result.Entities.Select(x => _mapper.Map<StateDto>(x)).ToList()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
