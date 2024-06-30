using BackendBase.Dto;
using BackendBase.Models;
using MathNet.Numerics.Statistics.Mcmc;

namespace BackendBase.Interfaces;

public interface IActivityService
{
    Task<Activity> AddEntity(Activity entity);
    Task<ActivityDto> GetById(Guid id);
    Task<ICollection<ActivityDto>> GetAll();
    Task<Activity> Update(Activity entity);
    Task<bool> DeleteById(Guid entityId);
    Task<PaginationDto<ActivityDto>> Search(SearchDto searchDto);
}