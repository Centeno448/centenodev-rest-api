using System;
using System.Security.Claims;

namespace CentenoDev.API.Authorization
{
    public interface IJwtAuthManager
    {
        JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now);

        void DeleteCachedRefreshToken(string username);

        bool IsRefreshTokenValid(string username, string refreshToken, string accessToken)
    }
}