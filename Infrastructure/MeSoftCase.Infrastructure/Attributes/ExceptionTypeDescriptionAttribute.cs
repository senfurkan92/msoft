namespace MeSoftCase.Infrastructure.Attributes
{
    /// <summary>
    /// error messages of exceptionTypes as attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ExceptionTypeDescriptionAttribute : Attribute
    {
        public string Description { get; }

        public ExceptionTypeDescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}
