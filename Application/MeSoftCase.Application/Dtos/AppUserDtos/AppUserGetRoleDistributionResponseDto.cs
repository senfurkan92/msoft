namespace MeSoftCase.Application.Dtos.AppUserDtos
{
    /// <summary>
    /// role distribution of appusers
    /// </summary>
    /// <param name="TotalUser"></param>
    /// <param name="Admin"></param>
    /// <param name="Manager"></param>
    /// <param name="Customer"></param>
    public record AppUserGetRoleDistributionResponseDto(int TotalUser, int Admin, int Manager, int Customer);
}
