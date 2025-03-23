using MediatR;
using MeSoftCase.Application.Features.Mediator.Results.AppUserResults;

namespace MeSoftCase.Application.Features.Mediator.Queries.AppUserQueries
{
    /// <summary>
    /// get role distribution of users
    /// </summary>
    public record AppUserGetRoleDistributionQuery() : IRequest<AppUserGetRoleDistributionResult>;
}
