using BackendBase.Data;
using BackendBase.Helpers;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Services
{
    public class StateService : CRUDServiceBase<State>, IStateService
    {
        public StateService(StateRepository repository)
        {
            _repository = repository;
        }
    }
}
