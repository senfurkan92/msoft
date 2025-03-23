using Mapster;
using MeSoftCase.Application.Dtos.AppUserDtos;
using MeSoftCase.Application.Interfaces;
using MeSoftCase.Domain.Entities;
using MeSoftCase.Infrastructure.Enums;
using MeSoftCase.Infrastructure.Helpers;
using MeSoftCase.Infrastructure.Interfaces;
using MeSoftCase.Infrastructure.Models;
using MeSoftCase.Infrastructure.Persistance.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MeSoftCase.Infrastructure.Services
{
    public class AppUserService(IMemoryCache cache, IHttpContextAccessor httpContextAccessor, IBlockedIpService blockedIpService, ITokenService tokenService, IAppUserRepository appUserRepository, UserManager<AppUser> userManager) : IAppUserService
    {
        /// <summary>
        /// signin with password and email, then getting jwt
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<AppUserSignInResponseDto> SignIn(AppUserSignInRequestDto dto)
        {
            var ipAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            await CheckIsAppropriateSequentialRequest("AppUser_SignIn", ipAddress);

            if (dto.IsMalicious())
                throw new CustomException(ExceptionType.MaliciousInputDetected);

            var user = await userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                throw new CustomException(ExceptionType.AppUserServiceSignInNotFound);

            if (user.LockoutEnd is not null && user.LockoutEnd >= DateTimeOffset.UtcNow)
                throw new CustomException(ExceptionType.AppUserServiceSignInLocked);

            if (!await userManager.CheckPasswordAsync(user, dto.Password))
            {
                await userManager.AccessFailedAsync(user);
                throw new CustomException(ExceptionType.AppUserServiceSignInInvalidPassword);
            }

            cache.Remove($"AppUser_SignIn:{ipAddress}");

            await userManager.SetLockoutEndDateAsync(user, null);
            await userManager.ResetAccessFailedCountAsync(user);

            var token = await GetToken(user);

            return token.Adapt<AppUserSignInResponseDto>();
        }

        /// <summary>
        /// signin with password and email, then getting jwt
        /// deciding user role based on referral code
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<AppUserSignUpResponseDto> SignUp(AppUserSignUpRequestDto dto)
        {
            var ipAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            await CheckIsAppropriateSequentialRequest("AppUser_SignUp", ipAddress);

            if (dto.IsMalicious())
                throw new CustomException(ExceptionType.MaliciousInputDetected);

            if (dto.ReferralCode != null)
                CheckReferralCode(dto.ReferralCode, dto.Email);

            var user = await userManager.FindByEmailAsync(dto.Email);

            if (user != null)
                throw new CustomException(ExceptionType.AppUserServiceSignUpEmailAlreadyExists);

            user = new AppUser
            {
                Email = dto.Email,
                Id = Guid.NewGuid().ToString("N"),
                UserName = dto.Email
            };

            var createResult = await userManager.CreateAsync(user, dto.Password);

            if (!createResult.Succeeded)
                throw new CustomException(ExceptionType.InternalServerError, null, createResult.Errors.Select(x => x.Description).ToList());

            cache.Remove($"AppUser_SignUp:{ipAddress}");

            if (dto.ReferralCode != null)
                await userManager.AddToRoleAsync(user, "Manager");
            else
                await userManager.AddToRoleAsync(user, "Customer");

            var token = await GetToken(user);

            return token.Adapt<AppUserSignUpResponseDto>();
        }

        /// <summary>
        /// generate referaal url to invite users
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public async Task<AppUserGenerateReferralUrlResponseDto> GenerateReferralUrl(AppUserGenerateReferralUrlRequestDto dto)
        {
            if (dto.IsMalicious())
                throw new CustomException(ExceptionType.MaliciousInputDetected);

            var user = await userManager.FindByEmailAsync(dto.Email);

            if (user != null)
                throw new CustomException(ExceptionType.AppUserServiceSignUpEmailAlreadyExists);

            var referralCode = GenerateReferralCode(dto.Email);

            var httpContext = httpContextAccessor.HttpContext;

            var referralUrl = $"{httpContext!.Request.Scheme}://{httpContext.Request.Host}/account/?referralCode={referralCode}";

            return new AppUserGenerateReferralUrlResponseDto(referralUrl);
        }

        /// <summary>
        /// get role distribution of users
        /// </summary>
        /// <returns></returns>
        public Task<AppUserGetRoleDistributionResponseDto> GetRoleDistribution()
        {
            return appUserRepository.GetRoleDistribution();
        }

        /// <summary>
        /// list of users
        /// </summary>
        /// <returns></returns>
        public Task<List<AppUserListResponseDto>> List()
        {
            return appUserRepository.List();
        }

        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(string id)
        {
            var appUser = await userManager.FindByIdAsync(id);

            if (appUser == null) return;

            if (await userManager.IsInRoleAsync(appUser, "Admin")) return;

            await userManager.DeleteAsync(appUser);
        }

        /// <summary>
        /// Checks if a user's IP address has exceeded the allowed number of failed login attempts in sequence. 
        /// If the limit is exceeded (10 consecutive failed logins), the IP address is added to the blacklist.
        /// </summary>
        /// <param name="cachePrefix">The prefix used to build the cache key for tracking login attempts.</param>
        /// <param name="ipAddress">The IP address of the user attempting to login.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task CheckIsAppropriateSequentialRequest(string cachePrefix, string? ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress))
                return;

            var cacheKey = $"{cachePrefix}:{ipAddress}";

            var sequentialRequest = cache.Get<int?>(cacheKey);

            if (sequentialRequest == null)
            {
                cache.Set(cacheKey, 1);
                return;
            }

            var currentRequestCount = sequentialRequest.Value + 1;

            if (currentRequestCount > 9)
            {
                cache.Remove(cacheKey);
                await blockedIpService.AddToBlackList(ipAddress);
                throw new CustomException(ExceptionType.IpBlocked);
            }

            cache.Set(cacheKey, currentRequestCount);
        }

        /// <summary>
        /// Generates a JWT token for the given application user based on their claims and roles.
        /// </summary>
        /// <param name="user">The application user for whom the token will be generated.</param>
        /// <returns>A task representing the asynchronous operation, containing the generated token response.</returns>
        private async Task<GetUserTokenResponse> GetToken(AppUser user)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email ?? "-"));
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? "-"));

            var roles = await userManager.GetRolesAsync(user);

            if (roles is { Count: > 0 })
                roles.ToList().ForEach(x => claims.Add(new Claim(ClaimTypes.Role, x)));

            var token = tokenService.GetUserToken(new GetUserTokenRequest
            {
                Claims = claims
            });

            return token;
        }

        /// <summary>
        /// generate referral code based on date and vi, consequently
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private string GenerateReferralCode(string email)
        {
            var date = DateTimeOffset.UtcNow.ToString("yyyyMMddHHmmssff");
            var code = EncryptionHelper.EncryptString(email.ToLower(), date) + $"###{date}";
            code = EncryptionHelper.EncryptString(code);
            return code;
        }

        /// <summary>
        /// check referral code based on vi and date, consequently
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private void CheckReferralCode(string code, string email)
        {
            string decodedEmail = string.Empty;

            try
            {
                code = EncryptionHelper.DecryptString(code);
                var date = code.Split("###")[1];
                decodedEmail = EncryptionHelper.DecryptString(code.Split("###")[0], date);
            }
            catch (Exception)
            {
                throw new CustomException(ExceptionType.AppUserInvalidReferralCode);
            }

            if (decodedEmail != email.ToLower())
                throw new CustomException(ExceptionType.AppUserInvalidReferralEmail);
        }
    }
}
