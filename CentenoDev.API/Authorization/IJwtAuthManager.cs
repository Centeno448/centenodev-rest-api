using System;
using System.Security.Claims;

namespace CentenoDev.API.Authorization
{
    public interface IJwtAuthManager
    {
        JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now);
    }
}