using MediatR;
using MeSoftCase.Application.Features.Mediator.Commands.BlockedIpCommands;
using MeSoftCase.Application.Interfaces;

namespace MeSoftCase.Application.Features.Mediator.Handlers.BlockedIpHandlers
{
    /// <summary>
    /// remove ip on blacklist
    /// </summary>
    /// <param name="blockedIpService"></param>
    public class BlockedIpRemoveFromBlackListHandler(IBlockedIpService blockedIpService) : IRequestHandler<BlockedIpRemoveFromBlackListCommand>
    {
        public async Task Handle(BlockedIpRemoveFromBlackListCommand request, CancellationToken cancellationToken)
        {
            await blockedIpService.RemoveFromBlackList(request.ipAddress);
        }
    }
}
