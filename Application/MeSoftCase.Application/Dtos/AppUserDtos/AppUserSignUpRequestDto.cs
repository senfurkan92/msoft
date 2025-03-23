namespace MeSoftCase.Application.Dtos.AppUserDtos
{
    /// <summary>
    /// signup with email and password
    /// deciding on user role based on referral code
    /// </summary>
    /// <param name="Email"></param>
    /// <param name="Password"></param>
    /// <param name="ReferralCode"></param>
    public record AppUserSignUpRequestDto(
            string Email,
            string Password,
            string? ReferralCode
        );
}
