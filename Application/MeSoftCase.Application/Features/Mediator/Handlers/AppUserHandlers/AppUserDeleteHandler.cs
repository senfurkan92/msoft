using MediatR;
using MeSoftCase.Application.Features.Mediator.Commands.AppUserCommands;
using MeSoftCase.Application.Interfaces;

namespace MeSoftCase.Application.Features.Mediator.Handlers.AppUserHandlers
{
    /// <summary>
    /// appuser delete handler
    /// </summary>
    /// <param name="appUserService"></param>
    public class AppUserDeleteHandler(IAppUserService appUserService) : IRequestHandler<AppUserDeleteCommand>
    {
        public async Task Handle(AppUserDeleteCommand request, CancellationToken cancellationToken)
        {
            await appUserService.Delete(request.Id);
        }
    }
}
