using MediatR;
using MeSoftCase.Application.Dtos.AppUserDtos;
using MeSoftCase.Application.Features.Mediator.Commands.AppUserCommands;
using MeSoftCase.Application.Features.Mediator.Results.AppUserResults;
using MeSoftCase.Application.Interfaces;

namespace MeSoftCase.Application.Features.Mediator.Handlers.AppUserHandlers
{
    /// <summary>
    /// generate referral url based on user email
    /// </summary>
    /// <param name="appUserService"></param>
    public class AppUserGenerateReferralUrlHandler(IAppUserService appUserService) : IRequestHandler<AppUserGenerateReferralUrlCommand, AppUserGenerateReferralUrlResult>
    {
        public async Task<AppUserGenerateReferralUrlResult> Handle(AppUserGenerateReferralUrlCommand request, CancellationToken cancellationToken)
        {
            var response = await appUserService.GenerateReferralUrl(new AppUserGenerateReferralUrlRequestDto(request.Email));

            return new AppUserGenerateReferralUrlResult(response.ReferralUrl);
        }
    }
}
