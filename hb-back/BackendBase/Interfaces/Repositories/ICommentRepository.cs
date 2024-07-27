using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Repositories;

public interface ICommentRepository
{
    Task<Comment> AddEntity(Comment entity);
    Task<Comment> GetById(Guid id);
    Task<ICollection<Comment>> GetAll();
    Task<Comment> UpdateEntity(Comment entity);
    Task<bool> DeleteById(Guid entityId);
    Task<Pagination<Comment>> Search(SearchDto searchDto);
    Task<bool> Save();
    IQueryable<Comment> IncludeChildren(IQueryable<Comment> query);
}
