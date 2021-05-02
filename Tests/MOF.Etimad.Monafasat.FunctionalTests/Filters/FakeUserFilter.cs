using Microsoft.AspNetCore.Mvc.Filters;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.FunctionalTests.Filters
{
    public class FakeUserFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, UserSettings.UserId),
            new Claim(ClaimTypes.Name, UserSettings.Name),
            new Claim(ClaimTypes.Email, UserSettings.UserEmail),
            new Claim(ClaimTypes.Role, RoleNames.DataEntry)
        }));

            await next();
        }
    }
}
