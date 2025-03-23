namespace MeSoftCase.Application.Models
{
    /// <summary>
    /// accessing the values ​​obtained from jwt as scope
    /// </summary>
    public class ApiContext
    {
        public string? AppUserId { get; set; }
        public string? AppUserEmail { get; set; }
        public string? CurrentRemoteIp { get; set; }
    }
}
