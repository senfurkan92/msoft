using MediatR;
using MeSoftCase.Application.Features.Mediator.Queries.AppUserQueries;
using Microsoft.AspNetCore.Mvc;

namespace MeSoftCase.WebUI.Components
{
    public class UserRoleDistributionComponent(IMediator mediator) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await mediator.Send(new AppUserGetRoleDistributionQuery());

            (int TotalUser, int AdminCount, int ManagerCount, int CostumerCount) data = (response.TotalUser, response.Admin, response.Manager, response.Customer);

            return View("~/Components/UserRoleDistributionComponent.cshtml", data);
        }
    }
}
