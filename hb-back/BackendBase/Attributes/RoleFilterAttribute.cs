using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using BackendBase.Models.Enum;
using BackendBase.Models;

namespace BackendBase.Attributes
{
    public class RoleFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly RoleUserEnum _permissionLevel;

        public RoleFilterAttribute(RoleUserEnum permissionLevel)
        {
            _permissionLevel = permissionLevel;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (await Validate(context))
                await next();
        }

        private async Task<bool> Validate(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("user", out var objectModel);

            var model = objectModel as User;

            if (model == null)
                return _permissionLevel == RoleUserEnum.User;

            return model.Role >= _permissionLevel;
        }
    }
}
