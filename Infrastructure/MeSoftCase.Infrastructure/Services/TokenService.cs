using MeSoftCase.Infrastructure.Interfaces;
using MeSoftCase.Infrastructure.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MeSoftCase.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptionsMonitor<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.CurrentValue;
        }

        public GetUserTokenResponse GetUserToken(GetUserTokenRequest request)
        {
            var now = DateTimeOffset.UtcNow;

            var accessTokenExpire = now.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes);
            var refreshTokenExpire = now.AddMinutes(_jwtSettings.RefreshTokenExpirationMinutes);

            var accessToken = GetToken(request.Claims, accessTokenExpire);
            var refreshToken = GetRefreshToken(refreshTokenExpire);

            return new GetUserTokenResponse
            {
                AccessToken = accessToken,
                AccessTokenUnixExpire = accessTokenExpire.ToUnixTimeSeconds(),
                AccessTokenExpire = accessTokenExpire,
                RefreshToken = refreshToken,
                RefreshTokenUnixExpire = refreshTokenExpire.ToUnixTimeSeconds(),
                RefreshTokenExpire = refreshTokenExpire
            };
        }

        private string GetToken(List<Claim>? claims, DateTimeOffset expire)
        {
            var now = DateTimeOffset.UtcNow;

            claims = claims ?? new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Iss, _jwtSettings.Issuer));
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, _jwtSettings.Audience));
            claims.Add(new Claim(JwtRegisteredClaimNames.Exp, expire.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                expires: expire.DateTime,
                notBefore: now.DateTime,
                claims: claims,
                signingCredentials: GetSigningCredentials(_jwtSettings.SecurityKey, _jwtSettings.SecurityAlgorithms)
            );

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(securityToken);

            return token;
        }

        private SigningCredentials GetSigningCredentials(string key, string algorithm)
        {
            return new SigningCredentials(GetSecurityKey(key), algorithm);
        }

        private SecurityKey GetSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        private string GetRefreshToken(DateTimeOffset expire)
        {
            var numberByte = new Byte[32];

            var rnd = RandomNumberGenerator.Create();

            rnd.GetBytes(numberByte);

            var refreshtoken = Convert.ToBase64String(numberByte).Replace("#", "") + "#" + string.Join("", expire.ToUnixTimeSeconds().ToString().Reverse().ToList());

            return refreshtoken;
        }
    }
}
