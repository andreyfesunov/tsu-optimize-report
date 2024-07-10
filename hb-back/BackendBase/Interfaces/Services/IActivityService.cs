using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface IActivityService
{
    Task<Activity> AddEntity(Activity entity);
    Task<ActivityDto> GetById(Guid id);
    Task<ICollection<ActivityDto>> GetAll();
    Task<Activity> Update(Activity entity);
    Task<bool> DeleteById(Guid entityId);
    Task<Pagination<ActivityDto>> Search(SearchDto searchDto);
}