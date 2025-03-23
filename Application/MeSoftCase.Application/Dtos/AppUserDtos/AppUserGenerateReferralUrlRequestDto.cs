namespace MeSoftCase.Application.Dtos.AppUserDtos
{
    /// <summary>
    /// generate referral url based on user email
    /// </summary>
    /// <param name="Email"></param>
    public record AppUserGenerateReferralUrlRequestDto(string Email);
}
