using MediatR;
using MeSoftCase.Application.Features.Mediator.Commands.AppUserCommands;
using MeSoftCase.Application.Features.Mediator.Results.AppUserResults;
using MeSoftCase.WebUI.Models;
using MeSoftCase.WebUI.Models.AppUserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MeSoftCase.WebUI.Areas
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IMediator mediator) : ControllerBase
    {
        [HttpPost("signIn")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<AppUserSignInResult>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse))]
        [ProducesResponseType(404, Type = typeof(ApiResponse))]
        [ProducesResponseType(500, Type = typeof(ApiResponse))]
        [Produces("application/json; charset=utf-8", "application/json")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel model)
        {
            var response = await mediator.Send(new AppUserSignInCommand(model.Email, model.Password));

            Response.Cookies.Append("MeSoftToken", response.AccessToken, new CookieOptions
            {
                Expires = response.AccessTokenExpire,
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return new ObjectResult(response)
            {
                StatusCode = 200,
            };
        }

        [HttpPost("signUp")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<AppUserSignUpResult>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse))]
        [ProducesResponseType(404, Type = typeof(ApiResponse))]
        [ProducesResponseType(500, Type = typeof(ApiResponse))]
        [Produces("application/json; charset=utf-8", "application/json")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
        {
            var response = await mediator.Send(new AppUserSignUpCommand(model.Email, model.Password, model.ReferralCode));

            Response.Cookies.Append("MeSoftToken", response.AccessToken, new CookieOptions
            {
                Expires = response.AccessTokenExpire,
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return new ObjectResult(response)
            {
                StatusCode = 200,
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("generateReferralUrl")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<AppUserGenerateReferralUrlResult>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse))]
        [ProducesResponseType(404, Type = typeof(ApiResponse))]
        [ProducesResponseType(500, Type = typeof(ApiResponse))]
        [Produces("application/json; charset=utf-8", "application/json")]
        public async Task<IActionResult> GenerateReferralUrl([FromBody] GenerateReferralUrlModel model)
        {
            var response = await mediator.Send(new AppUserGenerateReferralUrlCommand(model.Email));

            var result = new ApiResponse<AppUserGenerateReferralUrlResult>(HttpStatusCode.OK, true, response);

            return new ObjectResult(result)
            {
                StatusCode = 200,
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("delete/{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponse))]
        [ProducesResponseType(400, Type = typeof(ApiResponse))]
        [ProducesResponseType(404, Type = typeof(ApiResponse))]
        [ProducesResponseType(500, Type = typeof(ApiResponse))]
        [Produces("application/json; charset=utf-8", "application/json")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await mediator.Send(new AppUserDeleteCommand(id));

            var response = new ApiResponse(HttpStatusCode.OK, true);

            return new ObjectResult(response)
            {
                StatusCode = 200,
            };
        }
    }
}
