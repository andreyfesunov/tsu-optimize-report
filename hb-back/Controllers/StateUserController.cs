using BackendBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackendBase.Models;
using BackendBase.Helpers.CRUD;
using BackendBase.Dto.Report;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateUserController : CRUDControllerBase<StateUser, ReportListDto>
    {
        public StateUserController(IStateUserService service)
        : base(service)
        { }
    }
}
