﻿using BackendBase.Data;
using BackendBase.Helpers;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Services
{
    public class StateUserService : CRUDServiceBase<StateUser>, IStateUserService
    {
        public StateUserService(StateUserRepository repository)
        {
            _repository = repository;
        }
    }
}