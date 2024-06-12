using BackendBase.Dto;

namespace BackendBase.Middlewares;

public class UserInfoMiddleware
{
    private readonly RequestDelegate _next;

    public UserInfoMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, UserInfo userInfo)
    {
        var userId = context.User.FindFirst("id")?.Value;

        if (userId != null) userInfo.SetUserId(userId);

        await _next(context);
    }
}