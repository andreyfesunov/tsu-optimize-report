using BackendBase.Dto;
using BackendBase.Helpers;
using BackendBase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackendBase.Models;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituteController : CRUDControllerBase<Institute>
    {
        public InstituteController(IInstituteService serivce)
            : base(serivce)
        {
        }
    }
}
