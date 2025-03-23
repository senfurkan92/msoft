using MediatR;

namespace MeSoftCase.Application.Features.Mediator.Commands.AppUserCommands
{
    /// <summary>
    /// appuser delete command
    /// </summary>
    /// <param name="Id"></param>
    public record AppUserDeleteCommand(
            string Id
        ) : IRequest;
}
