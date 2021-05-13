using AutoMapper;
using CentenoDev.API.Authorization;
using CentenoDev.API.Entities;
using CentenoDev.API.Models.Account;
using CentenoDev.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CentenoDev.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IJwtAuthManager _jwtAuthManager;

        public AccountController(IMapper mapper, IAccountService accountService, ILogger<AccountController> logger, IJwtAuthManager jwtAuthManager)
        {
            _logger = logger;
            _accountService = accountService;
            _mapper = mapper;
            _jwtAuthManager = jwtAuthManager;
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
    }
}
