using MeSoftCase.Application.Models;
using System.Security.Claims;

namespace MeSoftCase.WebUI.Middlewares
{
    /// <summary>
    /// transferring jwt values
    /// </summary>
    /// <param name="_next"></param>
    public class ApiContextMiddleware(RequestDelegate _next)
    {
        public async Task Invoke(HttpContext context, ApiContext apiContext)
        {
            if (context.User.Identity!.IsAuthenticated)
            {
                apiContext.AppUserId = context.User.Claims.LastOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                apiContext.AppUserEmail = context.User.Claims.LastOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            }

            apiContext.CurrentRemoteIp = context.Connection.RemoteIpAddress?.ToString();

            await _next(context);
        }
    }
}
