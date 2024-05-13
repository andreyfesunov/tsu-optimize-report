using BackendBase.Dto;
using BackendBase.Helpers.CRUD;
using BackendBase.Models;

namespace BackendBase.Interfaces
{
    public interface IStateService : ICRUDServiceBase<State, StateDto>
    {
    }
}
