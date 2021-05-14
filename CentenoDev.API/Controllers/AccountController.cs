using AutoMapper;
using CentenoDev.API.Authorization;
using CentenoDev.API.Entities;
using CentenoDev.API.Models.Account;
using CentenoDev.API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CentenoDev.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IDistributedCache _cache;

        public AccountController(IMapper mapper, 
            IAccountService accountService, 
            IJwtAuthManager jwtAuthManager,
            IDistributedCache distributedCache)
        {
            _accountService = accountService;
            _mapper = mapper;
            _jwtAuthManager = jwtAuthManager;
            _cache = distributedCache;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<LoginResult>> Login([FromBody] Account account)
        {
            var entity = _mapper.Map<AccountEntity>(account);

            var loggedIn = await _accountService.LoginUser(entity);

            if (loggedIn == null)
                return NotFound();

            var role = "user";

            if (loggedIn.IsAdmin)
                role = "admin";

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, loggedIn.Username),
                new Claim(ClaimTypes.Role, role)
            };

            var jwtResult = _jwtAuthManager.GenerateTokens(loggedIn.Username, claims, DateTime.Now);

            var result = new LoginResult()
            {
                AccessToken = jwtResult.AccessToken,
                Username = loggedIn.Username,
                Role = role,
                RefreshToken = jwtResult.RefreshToken.TokenString
            };

            return Ok(result);
        }


        [Authorize]
        [HttpPost("logout")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public  ActionResult Logout()
        {
            var username = User.Identity.Name;

            _jwtAuthManager.DeleteCachedRefreshToken(username);

            return Ok();
        }


        [Authorize]
        [HttpPost("refresh-token")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var username = User.Identity.Name;

            if (string.IsNullOrWhiteSpace(request.RefreshToken))
            {
                return Unauthorized();
            }

            var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
            bool isValid = _jwtAuthManager.IsRefreshTokenValid(username, request.RefreshToken, accessToken);

            if (!isValid)
            {
                return Unauthorized("Refresh token is not valid.");
            }

            var jwtResult = _jwtAuthManager.GenerateTokens(username, User.Claims.ToArray(), DateTime.Now);
            
            return Ok(new LoginResult
            {
                Username = username,
                Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString
            });
        }
    }
}
