using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Services
{
    public class StateService : CRUDServiceBase<State, StateDto>, IStateService
    {
        public StateService(StateRepository repository)
        {
            _repository = repository;
        }
    }
}
