using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<User?> GetByEmail(string email);
    Task<User> GetById(Guid id);
    Task<ICollection<User>> GetAll();
    Task<Pagination<User>> Search(SearchDto searchDto);
    public Task<User> AddEntity(User entity);
}
