using MediatR;
using MeSoftCase.Application.Features.Mediator.Results.AppUserResults;

namespace MeSoftCase.Application.Features.Mediator.Queries.AppUserQueries
{
    /// <summary>
    /// list of appusers
    /// </summary>
    public record AppUserListQuery() : IRequest<AppUserListResult>;
}
