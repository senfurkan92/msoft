using MeSoftCase.Application.Models;
using MeSoftCase.Infrastructure.Enums;
using MeSoftCase.Infrastructure.Models;
using MeSoftCase.WebUI.Models;
using Newtonsoft.Json;
using System.Net;

namespace MeSoftCase.WebUI.Middlewares
{
    /// <summary>
    /// handles exceptions throughout the application pipeline
    /// </summary>
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JsonSerializerSettings _settings;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
            _settings = new JsonSerializerSettings();
            _settings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            _settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        }

        public async Task Invoke(HttpContext context, ApiContext apiContext)
        {
            try
            {
                await _next(context);
            }
            catch (CustomException ex)
            {
                await HandleResponseExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleResponseExceptionAsync(HttpContext context, CustomException ex)
        {
            var response = new ApiResponse(statusCode: HttpStatusCode.BadRequest, isSuccess: false, message: ex.Message, exceptionType: ex.Type, null, errors: ex.Errors);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, _settings));
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = new ApiResponse(statusCode: HttpStatusCode.InternalServerError, isSuccess: false, message: ex.Message, exceptionType: ExceptionType.InternalServerError, exception: ex);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, _settings));
        }
    }
}
