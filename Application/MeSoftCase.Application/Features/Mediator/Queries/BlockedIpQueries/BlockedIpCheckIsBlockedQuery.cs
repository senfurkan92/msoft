using MediatR;
using MeSoftCase.Application.Features.Mediator.Results.BlockedIpResults;

namespace MeSoftCase.Application.Features.Mediator.Queries.BlockedIpQueries
{
    /// <summary>
    /// check whether ip is blocked
    /// </summary>
    /// <param name="ipAddress"></param>
    public record BlockedIpCheckIsBlockedQuery(string ipAddress) : IRequest<BlockedIpCheckIsBlockedResult>;
}
