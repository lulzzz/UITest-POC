using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.FunctionalTests.Helpers
{
    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {

        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //var claims = new[] {
            // new Claim(ClaimTypes.Role, RoleNames.supplier),
            //};
            var claims = GetAllClaims();
            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "Test");

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }

        private Claim[] GetAllClaims()
        {
            List<Claim> claims = new List<Claim>();
            var _roles = typeof(RoleNames).GetFields();
            foreach (var role in _roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, typeof(RoleNames).GetField(role.Name).GetValue(null).ToString()));
            }
            claims.Add(new Claim(ClaimTypes.NameIdentifier, "1"));
            claims.Add(new Claim(ClaimTypes.Role, RoleNames.IndexPolicy));
            claims.RemoveAll(c => c.Value == RoleNames.supplier);
            return claims.ToArray();
        }
    }
}
