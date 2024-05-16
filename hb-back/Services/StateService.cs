using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Dto.CreateDto;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.UserModel;

namespace BackendBase.Services
{
    public class StateService : CRUDServiceBase<State, StateDto>, IStateService
    {
        private readonly StateRepository _stateRepository;

        public StateService(StateRepository repository)
        {
            _repository = repository;
            _stateRepository = repository;
        }

        public async Task<State> AddStateWithCreateDto(StateCreateDto stateCreateDto)
        {
            return await _stateRepository.AddStateWithCreateDto(stateCreateDto);
        }
    }
}
