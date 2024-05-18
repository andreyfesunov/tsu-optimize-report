using BackendBase.Dto;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkController : CRUDControllerBase<Work, WorkDto>
{
    public WorkController(IWorkService service) : base(service)
    {
    }
}