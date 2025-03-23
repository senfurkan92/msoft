using MediatR;
using MeSoftCase.Application.Features.Mediator.Queries.BlockedIpQueries;
using MeSoftCase.Infrastructure.Enums;
using MeSoftCase.WebUI.Models;
using Newtonsoft.Json;
using System.Net;

namespace MeSoftCase.WebUI.Middlewares
{
    /// <summary>
    /// check whether ip on blacklist
    /// blocked whether true
    /// </summary>
    public class BlackListMiddleware
    {
        private RequestDelegate _next;
        private readonly JsonSerializerSettings _settings;

        public BlackListMiddleware(RequestDelegate next)
        {
            _next = next;
            _settings = new JsonSerializerSettings();
            _settings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            _settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        }

        public async Task Invoke(HttpContext context, IConfiguration configuration, IMediator mediator)
        {
            if (context.Request.Path.HasValue && context.Request.Path.Value.ToLower().StartsWith("/blocked"))
            {
                await _next(context);
            }
            else
            {
                var remoteIp = context.Connection.RemoteIpAddress?.ToString();

                var isBlocked = !string.IsNullOrEmpty(remoteIp)
                    ? (await mediator.Send(new BlockedIpCheckIsBlockedQuery(remoteIp))).isBlocked
                    : false;

                if (isBlocked)
                {
                    var path = context.Request.Path.Value;
                    var isApiController = path?.StartsWith("/api") ?? false;

                    if (isApiController)
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;

                        var response = new ApiResponse(statusCode: HttpStatusCode.BadRequest, isSuccess: false, exceptionType: ExceptionType.IpBlocked);

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(response, _settings));
                    }
                    else
                    {
                        context.Response.Redirect("/blocked");

                        return;
                    }
                }
                else
                {
                    await _next(context);
                }
            }
        }
    }
}
