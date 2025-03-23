namespace MeSoftCase.Application.Features.Mediator.Results.AppUserResults
{
    public record AppUserSignUpResult(
            string AccessToken,
            long AccessTokenUnixExpire,
            DateTimeOffset AccessTokenExpire,
            string RefreshToken,
            long RefreshTokenUnixExpire,
            DateTimeOffset RefreshTokenExpire
        );
}
