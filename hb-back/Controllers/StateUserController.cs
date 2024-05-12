using BackendBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackendBase.Models;
using BackendBase.Helpers.CRUD;

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
    }
}
