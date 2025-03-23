using MediatR;
using MeSoftCase.Application.Features.Mediator.Results.BlockedIpResults;

namespace MeSoftCase.Application.Features.Mediator.Queries.BlockedIpQueries
{
    /// <summary>
    /// list of blockedIps
    /// </summary>
    public record BlockedIpListQuery() : IRequest<BlockedIpListResult>;
}
