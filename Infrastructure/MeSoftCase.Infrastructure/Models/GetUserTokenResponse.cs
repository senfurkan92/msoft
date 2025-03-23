namespace MeSoftCase.Infrastructure.Models
{
    /// <summary>
    /// represents the response containing the access and refresh tokens along with their expiration details
    /// </summary>
    public class GetUserTokenResponse
    {
        public string AccessToken { get; set; } = null!;
        public long AccessTokenUnixExpire { get; set; }
        public DateTimeOffset AccessTokenExpire { get; set; }
        public string RefreshToken { get; set; } = null!;
        public long RefreshTokenUnixExpire { get; set; }
        public DateTimeOffset RefreshTokenExpire { get; set; }
    }
}
