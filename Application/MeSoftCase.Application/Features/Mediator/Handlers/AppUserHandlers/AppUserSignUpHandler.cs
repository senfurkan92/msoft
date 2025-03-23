using Mapster;
using MediatR;
using MeSoftCase.Application.Dtos.AppUserDtos;
using MeSoftCase.Application.Features.Mediator.Commands.AppUserCommands;
using MeSoftCase.Application.Features.Mediator.Results.AppUserResults;
using MeSoftCase.Application.Interfaces;

namespace MeSoftCase.Application.Features.Mediator.Handlers.AppUserHandlers
{
    /// <summary>
    /// signup with email and password
    /// deciding on user role based on referral code 
    /// </summary>
    /// <param name="appUserService"></param>
    public class AppUserSignUpHandler(IAppUserService appUserService) : IRequestHandler<AppUserSignUpCommand, AppUserSignUpResult>
    {
        public async Task<AppUserSignUpResult> Handle(AppUserSignUpCommand request, CancellationToken cancellationToken)
        {
            var response = await appUserService.SignUp(new AppUserSignUpRequestDto(request.Email, request.Password, request.ReferralCode));

            return response.Adapt<AppUserSignUpResult>();
        }
    }
}
