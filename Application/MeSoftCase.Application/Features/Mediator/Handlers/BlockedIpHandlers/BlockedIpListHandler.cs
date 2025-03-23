using Mapster;
using MediatR;
using MeSoftCase.Application.Features.Mediator.Queries.BlockedIpQueries;
using MeSoftCase.Application.Features.Mediator.Results.BlockedIpResults;
using MeSoftCase.Application.Interfaces;

namespace MeSoftCase.Application.Features.Mediator.Handlers.BlockedIpHandlers
{
    /// <summary>
    /// list of blockedIps
    /// </summary>
    public class BlockedIpListHandler(IBlockedIpService blockedIpService) : IRequestHandler<BlockedIpListQuery, BlockedIpListResult>
    {
        public async Task<BlockedIpListResult> Handle(BlockedIpListQuery request, CancellationToken cancellationToken)
        {
            var result = await blockedIpService.List();

            return new BlockedIpListResult(
                    result.Adapt<List<BlockedIpListItemResult>>()
                );
        }
    }
}
