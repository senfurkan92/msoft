namespace MeSoftCase.Application.Dtos.AppUserDtos
{
    /// <summary>
    /// list of appusers
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="UserName"></param>
    /// <param name="Email"></param>
    /// <param name="LockoutEnd"></param>
    /// <param name="Roles"></param>
    public record AppUserListResponseDto(
            string Id,
            string UserName,
            string Email,
            DateTimeOffset? LockoutEnd,
            string Roles
        );
}
