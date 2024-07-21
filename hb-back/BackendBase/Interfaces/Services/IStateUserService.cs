using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces.Services
{
    public interface IStateUserService
    {
        Task<StateUser> GetById(Guid id);
        Task<Pagination<StateUser>> Search(SearchDto searchDto);
    }
}
