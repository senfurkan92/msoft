using MeSoftCase.Infrastructure.Enums;

namespace MeSoftCase.Infrastructure.Models
{
    /// <summary>
    /// represents a custom exception type used to handle application-specific errors
    /// </summary>
    public class CustomException : Exception
    {
        public ExceptionType Type { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }

        public CustomException(ExceptionType type, string? message = null, List<string>? errors = null)
        {
            Type = type;
            Message = message;
            Errors = errors;
        }
    }
}
