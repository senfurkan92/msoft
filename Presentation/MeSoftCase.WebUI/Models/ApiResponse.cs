using MeSoftCase.Infrastructure.Attributes;
using MeSoftCase.Infrastructure.Enums;
using System.Net;
using System.Reflection;

namespace MeSoftCase.WebUI.Models
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public List<string> Errors { get; set; } = new List<string>();
        public Dictionary<string, List<string>?>? ValidationErrors { get; set; }

        public ApiResponse(HttpStatusCode statusCode, bool isSuccess, string? message = null, ExceptionType? exceptionType = null, Exception? exception = null, List<string>? errors = null, Dictionary<string, List<string>?>? validationErrors = null)
        {
            StatusCode = statusCode;
            IsSuccess = isSuccess;

            if (message != null) Message = message;
            else if (exceptionType != null) Message = GetDescription(exceptionType.Value);
            else if (exception != null) Message = exception.Message;

            if (errors != null) Errors.AddRange(errors);
            if (exception != null) Errors.AddRange(GetInnerExceptions(exception));

            ValidationErrors = validationErrors;
        }

        private static List<string> GetInnerExceptions(Exception ex, List<string>? errors = null)
        {
            errors ??= new List<string>();
            errors.Add(ex.Message);

            if (ex.InnerException != null)
                GetInnerExceptions(ex.InnerException, errors);

            return errors;
        }

        private static string GetDescription(ExceptionType exceptionType)
        {
            var memberInfo = exceptionType.GetType().GetMember(exceptionType.ToString());
            if (memberInfo.Length > 0)
            {
                var attribute = memberInfo[0].GetCustomAttribute<ExceptionTypeDescriptionAttribute>();
                return attribute?.Description ?? exceptionType.ToString();
            }

            return exceptionType.ToString();
        }
    }

    public class ApiResponse<TData> : ApiResponse
    {
        public TData? Data { get; set; }

        public ApiResponse(HttpStatusCode statusCode, bool isSuccess, TData? data = default, string? message = null, ExceptionType? exceptionType = null, Exception? exception = null, List<string>? errors = null, Dictionary<string, List<string>?>? validationErrors = null) : base(statusCode, isSuccess, message, exceptionType, exception, errors, validationErrors)
        {
            Data = data;
        }
    }
}
