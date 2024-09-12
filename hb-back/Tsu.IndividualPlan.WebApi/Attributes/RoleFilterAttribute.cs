using Microsoft.AspNetCore.Mvc.Filters;
using Tsu.IndividualPlan.Domain.Enumerations;
using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.WebApi.Attributes;

public class RoleFilterAttribute(RoleUserEnum permissionLevel) : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        if (await Validate(context))
            await next();
    }

    private async Task<bool> Validate(ActionExecutingContext context)
    {
        context.ActionArguments.TryGetValue("user", out var objectModel);

        if (objectModel is not User model)
            return permissionLevel == RoleUserEnum.User;

        return model.Role >= permissionLevel;
    }
}