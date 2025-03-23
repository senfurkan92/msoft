using MediatR;
using MeSoftCase.Application.Features.Mediator.Queries.BlockedIpQueries;
using Microsoft.AspNetCore.Mvc;

namespace MeSoftCase.WebUI.Components
{
    public class BlockedIpListComponent(IMediator mediator) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await mediator.Send(new BlockedIpListQuery());

            return View("~/Components/BlockedIpListComponent.cshtml", response.Items);
        }
    }
}
