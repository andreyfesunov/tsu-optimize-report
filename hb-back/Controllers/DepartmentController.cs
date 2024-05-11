﻿using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : CRUDControllerBase<Department>
    {
        public DepartmentController(IDepartmentService service)
        : base(service)
        {
        }
    }
}
