using MediatR;
using MeSoftCase.Application.Features.Mediator.Queries.AppUserQueries;
using Microsoft.AspNetCore.Mvc;

namespace MeSoftCase.WebUI.Components
{
    public class UserListComponent(IMediator mediator) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await mediator.Send(new AppUserListQuery());

            return View("~/Components/UserListComponent.cshtml", response.Items);
        }
    }
}
