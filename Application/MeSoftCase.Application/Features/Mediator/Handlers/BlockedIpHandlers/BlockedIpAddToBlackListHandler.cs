using MediatR;
using MeSoftCase.Application.Features.Mediator.Commands.BlockedIpCommands;
using MeSoftCase.Application.Interfaces;

namespace MeSoftCase.Application.Features.Mediator.Handlers.BlockedIpHandlers
{
    /// <summary>
    /// add ip on blacklist
    /// </summary>
    /// <param name="blockedIpService"></param>
    public class BlockedIpAddToBlackListHandler(IBlockedIpService blockedIpService) : IRequestHandler<BlockedIpAddToBlackListCommand>
    {
        public async Task Handle(BlockedIpAddToBlackListCommand request, CancellationToken cancellationToken)
        {
            await blockedIpService.AddToBlackList(request.ipAddress);
        }
    }
}
