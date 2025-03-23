using Ganss.Xss;

namespace MeSoftCase.Infrastructure.Helpers
{
    /// <summary>
    /// analyzing whether contains malicious content
    /// </summary>
    public static class SanitizerHelper
    {
        private static HtmlSanitizer sanitizer = new HtmlSanitizer();

        /// <summary>
        /// analyzing whether text contains malicious content
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsMalicious(this string str)
        {
            if (string.IsNullOrEmpty(str)) return true;

            var a = sanitizer.Sanitize(str);

            return str != sanitizer.Sanitize(str);
        }

        /// <summary>
        /// analyzing whether model properties contain malicious content
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsMalicious<T>(this T str) where T : class
        {
            var stringProperties = typeof(T)
                .GetProperties()
                .Where(x => x.PropertyType == typeof(string) || Nullable.GetUnderlyingType(x.PropertyType) == typeof(string))
                .ToList();

            foreach (var stringProperty in stringProperties)
            {
                var value = stringProperty.GetValue(str, null) as string;

                if (string.IsNullOrEmpty(value)) continue;

                if (value != sanitizer.Sanitize(value)) return true;
            }

            return false;
        }

    }
}
