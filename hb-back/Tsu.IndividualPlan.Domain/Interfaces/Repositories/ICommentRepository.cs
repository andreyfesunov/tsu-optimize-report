using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Interfaces.Repositories;

public interface ICommentRepository
{
    Task<Comment> AddEntity(Comment entity);
    Task<Comment> GetById(Guid id);
    Task<Comment> UpdateEntity(Comment entity);
    Task<bool> Delete(Comment entity);
}