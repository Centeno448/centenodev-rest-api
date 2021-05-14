using CentenoDev.API.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CentenoDev.API.Authorization
{
    public class JwtAuthManager : IJwtAuthManager
    {
        private readonly JwtConfig _jwtTokenConfig;
        private readonly byte[] _secret;
        private readonly IDistributedCache _cache;

        public JwtAuthManager(JwtConfig jwtTokenConfig, IDistributedCache cache)
        {
            _jwtTokenConfig = jwtTokenConfig;
            _secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret);
            _cache = cache;
        }

        public JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now)
        {
            var jwtToken = new JwtSecurityToken(
                _jwtTokenConfig.Issuer,
                string.Empty,
                claims,
                expires: now.AddMinutes(_jwtTokenConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var refreshToken = new RefreshToken
            {
                AccessToken = accessToken,
                UserName = username,
                TokenString = GenerateRefreshTokenString(),
                ExpireAt = now.AddMinutes(_jwtTokenConfig.RefreshTokenExpiration)
            };

            var serializedRefreshToken = JsonConvert.SerializeObject(refreshToken);
            
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = refreshToken.ExpireAt
            };

            _cache.Set(username, Encoding.ASCII.GetBytes(serializedRefreshToken), options);

            return new JwtAuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public void DeleteCachedRefreshToken(string username)
        {
            _cache.Remove(username);
        }

        public bool IsRefreshTokenValid(string username, string refreshToken, string accessToken)
        {
            var bytes = _cache.Get(username);
            var cachedRefreshToken = Encoding.ASCII.GetString(bytes);
            var cachedToken = JsonConvert.DeserializeObject<RefreshToken>(cachedRefreshToken);

            bool tokenExistsInCache = cachedToken.TokenString == refreshToken;
            bool tokenIsNotExpired = cachedToken.ExpireAt <= DateTime.Now;
            bool tokenIsValidForAccessToken = cachedToken.AccessToken == accessToken;

            return  tokenExistsInCache && tokenIsNotExpired && tokenIsValidForAccessToken;
        }

        private static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
