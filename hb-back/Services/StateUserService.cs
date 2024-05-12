using BackendBase.Data;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;
using MathNet.Numerics.Statistics.Mcmc;
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
