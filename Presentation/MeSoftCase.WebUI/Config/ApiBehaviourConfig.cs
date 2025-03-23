using MeSoftCase.Infrastructure.Enums;
using MeSoftCase.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MeSoftCase.WebUI.Config
{
    public static class ApiBehaviourConfig
    {
        /// <summary>
        /// configures custom validation handling for model state in the API controllers
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCustomValidationModel(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = false;

                opt.InvalidModelStateResponseFactory = context =>
                {
                    var path = context.HttpContext.Request.Path.Value;
                    var isApiController = path?.StartsWith("/api") ?? false;

                    if (!isApiController) return null;

                    var modelState = context.ModelState;

                    var validationErrors = modelState.Where(x => x is { Value: not null, Value.Errors: not null })
                        .ToDictionary(x => ToCamelCase(x.Key), x => x.Value?.Errors.Select(x => x.ErrorMessage)
                        .ToList());

                    var response = new ApiResponse(
                            statusCode: HttpStatusCode.BadRequest,
                            isSuccess: false,
                            exceptionType: ExceptionType.Validation,
                            validationErrors: validationErrors
                        );

                    return new ObjectResult(response)
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        ContentTypes = { "application/json; charset=utf-8" }
                    };
                };
            });
        }

        private static string ToCamelCase(string input)
        {
            if (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
                return input;

            return char.ToLowerInvariant(input[0]) + input.Substring(1);
        }
    }
}
