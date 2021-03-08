using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DepsWebApp.Services;
using DepsWebApp.Filters;
using DepsWebApp.Models;
using System;

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
        /// <summary>
        /// Method that registers an incoming account on the platform
        /// </summary>
        /// <param name="account">Account model that will be registered in api</param>
        /// <exception cref="NotImplementedException">Method not implemented</exception>
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] Account account)
        {
            throw new NotImplementedException();
        }
    }
}
