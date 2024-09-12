using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.WebApi.Dto;

namespace Tsu.IndividualPlan.WebApi.Extensions.Entities;

public static class ActivityExtensions
{
    public static ActivityDto toDTO(this Activity activity)
    {
        return new ActivityDto(activity.Id, activity.Name, activity.Column);
    }
}