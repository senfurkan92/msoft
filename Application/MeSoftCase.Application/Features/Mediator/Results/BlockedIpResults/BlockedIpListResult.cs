namespace MeSoftCase.Application.Features.Mediator.Results.BlockedIpResults
{
    /// <summary>
    /// list of blockedIps
    /// </summary>
    public record BlockedIpListResult(
            List<BlockedIpListItemResult> Items
        );

    public record BlockedIpListItemResult(
            int Id,
            string IpAddress,
            DateTimeOffset CreatedAt
        );
}
