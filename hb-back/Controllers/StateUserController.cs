﻿using BackendBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackendBase.Models;
using BackendBase.Helpers.CRUD;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateUserController //: CRUDControllerBase<StateUser>
    {
        public StateUserController(IStateUserService serivce)
            //: base(serivce)
        {
        }
    }
}
