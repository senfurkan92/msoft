using Mapster;
using MediatR;
using MeSoftCase.Application.Dtos.AppUserDtos;
using MeSoftCase.Application.Features.Mediator.Commands.AppUserCommands;
using MeSoftCase.Application.Features.Mediator.Results.AppUserResults;
using MeSoftCase.Application.Interfaces;

namespace MeSoftCase.Application.Features.Mediator.Handlers.AppUserHandlers
{
    /// <summary>
    /// signin with email and password
    /// </summary>
    /// <param name="appUserService"></param>
    public class AppUserSignInHandler(IAppUserService appUserService) : IRequestHandler<AppUserSignInCommand, AppUserSignInResult>
    {
        public async Task<AppUserSignInResult> Handle(AppUserSignInCommand request, CancellationToken cancellationToken)
        {
            var response = await appUserService.SignIn(new AppUserSignInRequestDto(request.Email, request.Password));

            return response.Adapt<AppUserSignInResult>();
        }
    }
}
