using MeSoftCase.Infrastructure.Models;

namespace MeSoftCase.Infrastructure.Interfaces
{
    public interface ITokenService
    {
        /// <summary>
        /// generate token based on claims
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetUserTokenResponse GetUserToken(GetUserTokenRequest request);
    }
}
