using MediatR;
using MeSoftCase.Application.Features.Mediator.Results.AppUserResults;

namespace MeSoftCase.Application.Features.Mediator.Commands.AppUserCommands
{
    /// <summary>
    /// generate referral url based on user email
    /// </summary>
    /// <param name="Email"></param>
    public record AppUserGenerateReferralUrlCommand(string Email) : IRequest<AppUserGenerateReferralUrlResult>;
}
