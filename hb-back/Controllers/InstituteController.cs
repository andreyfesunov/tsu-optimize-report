using BackendBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackendBase.Models;
using BackendBase.Helpers.CRUD;
using BackendBase.Dto;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituteController : CRUDControllerBase<Institute, InstituteDto>
    {
        public InstituteController(IInstituteService service)
        : base(service)
        {
        }
    }
}
