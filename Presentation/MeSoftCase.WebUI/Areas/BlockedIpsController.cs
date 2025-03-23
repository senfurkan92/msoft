using MediatR;
using MeSoftCase.Application.Features.Mediator.Commands.BlockedIpCommands;
using MeSoftCase.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MeSoftCase.WebUI.Areas
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockedIpsController(IMediator mediator) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("delete/{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponse))]
        [ProducesResponseType(400, Type = typeof(ApiResponse))]
        [ProducesResponseType(404, Type = typeof(ApiResponse))]
        [ProducesResponseType(500, Type = typeof(ApiResponse))]
        [Produces("application/json; charset=utf-8", "application/json")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await mediator.Send(new BlockedIpRemoveFromBlackListCommand(id));

            var response = new ApiResponse(HttpStatusCode.OK, true);

            return new ObjectResult(response)
            {
                StatusCode = 200,
            };
        }
    }
}
