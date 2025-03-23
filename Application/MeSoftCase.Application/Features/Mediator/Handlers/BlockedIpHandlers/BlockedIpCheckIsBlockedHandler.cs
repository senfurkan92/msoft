using MediatR;
using MeSoftCase.Application.Features.Mediator.Queries.BlockedIpQueries;
using MeSoftCase.Application.Features.Mediator.Results.BlockedIpResults;
using MeSoftCase.Application.Interfaces;

namespace MeSoftCase.Application.Features.Mediator.Handlers.BlockedIpHandlers
{
    /// <summary>
    /// check wheher ip is blocked
    /// </summary>
    /// <param name="blockedIpService"></param>
    public class BlockedIpCheckIsBlockedHandler(IBlockedIpService blockedIpService) : IRequestHandler<BlockedIpCheckIsBlockedQuery, BlockedIpCheckIsBlockedResult>
    {
        public async Task<BlockedIpCheckIsBlockedResult> Handle(BlockedIpCheckIsBlockedQuery request, CancellationToken cancellationToken)
        {
            var result = await blockedIpService.CheckIsBlocked(request.ipAddress);

            return new BlockedIpCheckIsBlockedResult(result);
        }
    }
}
