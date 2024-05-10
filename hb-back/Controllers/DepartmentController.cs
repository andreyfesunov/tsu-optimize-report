using BackendBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackendBase.Models;
using BackendBase.Helpers.CRUD;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController //: CRUDControllerBase<Department>
    {
        public DepartmentController(IDepartmentService serivce)
        //: base(serivce)
        {
        }
    }
}
