using System.Security.Claims;
using BackendBase.Dto;

namespace BackendBase.Middlewares;

public class UserInfoMiddleware
{
    private readonly RequestDelegate _next;

    public UserInfoMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId != null)
        {
            var userInfo = new UserInfo
            {
                UserId = userId
            };

            context.Items["UserInfo"] = userInfo;
        }

        await _next(context);
    }
}