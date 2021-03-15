using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DepsWebApp.Services;
using DepsWebApp.Filters;
using DepsWebApp.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using DepsWebApp.Contracts;
using System.Net;
using DepsWebApp.Services.Interfaces;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// This controller is used for account operations(authorization, authentication...)
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [CustomExceptionFilter]
    public class AuthController : ControllerBase
    {
#pragma warning disable CS1591 
        public AuthController(IAccountCoordinatorService accountCoordinator,ILogger<AuthController> logger)
        {
            _accountCoordinator = accountCoordinator;
            _logger = logger;
        }
#pragma warning restore CS1591
        /// <summary>
        /// Method that registers an incoming account on the platform
        /// </summary>
        /// <param name="account">Account model that will be registered in api</param>
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        [ProducesResponseType(typeof(string),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<string>> Register([FromBody] AccountDto account)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
            var requestResult = await _accountCoordinator.RegisterAsync(account.Login,account.Password);
            _logger.LogInformation($"Account created [{requestResult}]");
            return Ok(requestResult);
        }
        private readonly IAccountCoordinatorService _accountCoordinator;
        private readonly ILogger<AuthController> _logger;
    }
}
