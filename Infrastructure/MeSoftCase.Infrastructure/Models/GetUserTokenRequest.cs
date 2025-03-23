using System.Security.Claims;

namespace MeSoftCase.Infrastructure.Models
{
    /// <summary>
    /// represents a request to retrieve a user token, including the claims associated with the user
    /// </summary>
    public class GetUserTokenRequest
    {
        public List<Claim>? Claims { get; set; }
    }
}
