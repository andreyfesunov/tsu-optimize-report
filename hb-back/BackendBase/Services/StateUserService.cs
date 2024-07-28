using BackendBase.Dto;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services;
using BackendBase.Models;

namespace BackendBase.Services
{
    public class StateUserService : IStateUserService
    {
        private readonly IStateUserRepository _stateUserRepository;

        public StateUserService(IStateUserRepository stateUserRepository)
        {
            _stateUserRepository = stateUserRepository;
        }


        public async Task<StateUser> GetById(Guid id)
            => await _stateUserRepository.GetById(id);

        public async Task<Pagination<StateUser>> Search(SearchDto searchDto)
            => await _stateUserRepository.Search(searchDto);

    }
}
