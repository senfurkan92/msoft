namespace MeSoftCase.Application.Dtos.BlockedIpDtos
{
    public record BlockedIpListResponseDto(
            int Id,
            string IpAddress,
            DateTimeOffset CreatedAt
        );
}
