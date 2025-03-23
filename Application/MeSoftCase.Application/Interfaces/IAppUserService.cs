using MeSoftCase.Application.Dtos.AppUserDtos;

namespace MeSoftCase.Application.Interfaces
{
    public interface IAppUserService
    {
        /// <summary>
        /// signin with password and email, then getting jwt
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<AppUserSignInResponseDto> SignIn(AppUserSignInRequestDto dto);

        /// <summary>
        /// signup with password and email, then getting jwt
        /// deciding appuser role bt referral code
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<AppUserSignUpResponseDto> SignUp(AppUserSignUpRequestDto dto);

        /// <summary>
        /// generating referral url based on user email
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<AppUserGenerateReferralUrlResponseDto> GenerateReferralUrl(AppUserGenerateReferralUrlRequestDto dto);

        /// <summary>
        /// get role distribution of users
        /// </summary>
        /// <returns></returns>
        Task<AppUserGetRoleDistributionResponseDto> GetRoleDistribution();

        /// <summary>
        /// list of users
        /// </summary>
        /// <returns></returns>
        Task<List<AppUserListResponseDto>> List();

        /// <summary>
        /// delet user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(string id);
    }
}
