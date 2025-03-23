using MediatR;
using MeSoftCase.Application.Features.Mediator.Results.AppUserResults;

namespace MeSoftCase.Application.Features.Mediator.Commands.AppUserCommands
{
    /// <summary>
    /// signin with email and password
    /// </summary>
    /// <param name="Email"></param>
    /// <param name="Password"></param>
    public record AppUserSignInCommand(
            string Email,
            string Password
        ) : IRequest<AppUserSignInResult>;
}
