namespace MeSoftCase.Infrastructure.Models
{
    /// <summary>
    /// represents the settings required for configuring JWT (JSON Web Token) authentication
    /// </summary>
    public class JwtSettings
    {
        public string SecurityKey { get; set; } = null!;
        public int AccessTokenExpirationMinutes { get; set; }
        public int RefreshTokenExpirationMinutes { get; set; }
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string SecurityAlgorithms { get; set; } = null!;
    }
}
