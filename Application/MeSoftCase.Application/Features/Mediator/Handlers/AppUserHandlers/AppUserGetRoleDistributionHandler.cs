using MediatR;
using MeSoftCase.Application.Features.Mediator.Queries.AppUserQueries;
using MeSoftCase.Application.Features.Mediator.Results.AppUserResults;
using MeSoftCase.Application.Interfaces;

namespace MeSoftCase.Application.Features.Mediator.Handlers.AppUserHandlers
{
    /// <summary>
    /// role distribution of appusers
    /// </summary>
    /// <param name="appUserService"></param>
    public class AppUserGetRoleDistributionHandler(IAppUserService appUserService) : IRequestHandler<AppUserGetRoleDistributionQuery, AppUserGetRoleDistributionResult>
    {
        public async Task<AppUserGetRoleDistributionResult> Handle(AppUserGetRoleDistributionQuery request, CancellationToken cancellationToken)
        {
            var response = await appUserService.GetRoleDistribution();

            return new AppUserGetRoleDistributionResult(response.TotalUser, response.Admin, response.Manager, response.Customer);
        }
    }
}
