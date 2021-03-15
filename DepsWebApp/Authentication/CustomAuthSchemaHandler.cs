using DepsWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DepsWebApp.Authentication
{

#pragma warning disable CS1591
    public class CustomAuthSchemaHandler : AuthenticationHandler<CustomAuthSchemaOptions>
    {
        public CustomAuthSchemaHandler(IOptionsMonitor<CustomAuthSchemaOptions> options,
           ILoggerFactory logger,
           UrlEncoder encoder,
           ISystemClock clock,
           IAccountCoordinatorService accountCoordinator):base(options,logger,encoder,clock)
        {
            _accountCoordinator = accountCoordinator;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!TryGetEncryptedAccFromRequest(Request, out var encryptedAcc)) 
                return AuthenticateResult.NoResult();
            try
            {
                if (await _accountCoordinator.GetUserAccount(encryptedAcc))
                {
                    return AuthenticateResult.Success(
                        new AuthenticationTicket(
                            new ClaimsPrincipal(
                                new ClaimsIdentity(new List<Claim>
                                {
                                new Claim(ClaimsIdentity.DefaultNameClaimType,encryptedAcc),
                                }, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType
                                )), CustomAuthSchema.Name));
                }
                else throw new AuthenticationException("Invalid credentials");
            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Invalid credentials!");
            }
        }
        private static bool TryGetEncryptedAccFromRequest(HttpRequest request, out string encryptedAcc)
        {
            encryptedAcc = null;
            if (request.Headers.ContainsKey(HeaderNames.Authorization))
            {
                encryptedAcc = request.Headers[HeaderNames.Authorization].FirstOrDefault();;
            }
            return !string.IsNullOrEmpty(encryptedAcc);
        }
        private readonly IAccountCoordinatorService _accountCoordinator;
    }
#pragma warning restore CS1591
}
