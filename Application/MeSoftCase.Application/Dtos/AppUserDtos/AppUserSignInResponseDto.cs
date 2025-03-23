namespace MeSoftCase.Application.Dtos.AppUserDtos
{
    /// <summary>
    /// jwt as signin response
    /// </summary>
    /// <param name="AccessToken"></param>
    /// <param name="AccessTokenUnixExpire"></param>
    /// <param name="AccessTokenExpire"></param>
    /// <param name="RefreshToken"></param>
    /// <param name="RefreshTokenUnixExpire"></param>
    /// <param name="RefreshTokenExpire"></param>
    public record AppUserSignInResponseDto(
            string AccessToken,
            long AccessTokenUnixExpire,
            DateTimeOffset AccessTokenExpire,
            string RefreshToken,
            long RefreshTokenUnixExpire,
            DateTimeOffset RefreshTokenExpire
        );
}
