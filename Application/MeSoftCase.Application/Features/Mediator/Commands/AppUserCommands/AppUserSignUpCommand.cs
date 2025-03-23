using MediatR;
using MeSoftCase.Application.Features.Mediator.Results.AppUserResults;

namespace MeSoftCase.Application.Features.Mediator.Commands.AppUserCommands
{
    /// <summary>
    /// signup with email and password
    /// deciding on user role based on referral code
    /// </summary>
    /// <param name="Email"></param>
    /// <param name="Password"></param>
    /// <param name="ReferralCode"></param>
    public record AppUserSignUpCommand(
            string Email,
            string Password,
            string? ReferralCode
        ) : IRequest<AppUserSignUpResult>;
}
