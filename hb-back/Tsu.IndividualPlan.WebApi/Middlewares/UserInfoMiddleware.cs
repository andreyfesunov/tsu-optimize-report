using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.WebApi.Middlewares;

public class UserInfoMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, UserInfo userInfo)
    {
        var userId = context.User.FindFirst("id")?.Value;

        if (userId != null)
            userInfo.SetUserId(userId);

        await next(context);
    }
}