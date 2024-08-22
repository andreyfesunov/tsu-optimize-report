using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Extensions.Entities;

public static class ActivityExtensions
{
    public static ActivityDto toDTO(this Activity activity) =>
        new ActivityDto(Id: activity.Id, Name: activity.Name, Column: activity.Column);
}
