namespace MeSoftCase.Application.Dtos.AppUserDtos
{
    /// <summary>
    /// signin with email and password
    /// </summary>
    /// <param name="Email"></param>
    /// <param name="Password"></param>
    public record AppUserSignInRequestDto(
            string Email,
            string Password
        );
}
