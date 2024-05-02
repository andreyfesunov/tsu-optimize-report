using BackendBase.Dto;
using BackendBase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackendBase.Models;
using BackendBase.Helpers.CRUD;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : CRUDControllerBase<State>
    {
        public StateController(IStateService serivce)
            : base(serivce)
        {
        }
    }
}
