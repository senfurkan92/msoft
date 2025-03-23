using Mapster;
using MediatR;
using MeSoftCase.Application.Features.Mediator.Queries.AppUserQueries;
using MeSoftCase.Application.Features.Mediator.Results.AppUserResults;
using MeSoftCase.Application.Interfaces;

namespace MeSoftCase.Application.Features.Mediator.Handlers.AppUserHandlers
{
    /// <summary>
    /// list of appusers
    /// </summary>
    /// <param name="appUserService"></param>
    public class AppUserListHandler(IAppUserService appUserService) : IRequestHandler<AppUserListQuery, AppUserListResult>
    {
        public async Task<AppUserListResult> Handle(AppUserListQuery request, CancellationToken cancellationToken)
        {
            var response = await appUserService.List();

            return new AppUserListResult(
                    response.Adapt<List<AppUserListItemResult>>()
                );
        }
    }
}
