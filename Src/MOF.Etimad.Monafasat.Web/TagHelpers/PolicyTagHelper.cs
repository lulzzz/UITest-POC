using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.TagHelpers
{
   [HtmlTargetElement(Attributes = "policy")]
   public class PolicyTagHelper : TagHelper
   {
      private readonly IAuthorizationService _authService;
      private readonly ClaimsPrincipal _principal;

      public PolicyTagHelper(IAuthorizationService authService, IHttpContextAccessor httpContextAccessor)
      {
         _authService = authService;
         _principal = httpContextAccessor.HttpContext.User;
      }

      public string Policy { get; set; }

      public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
      {
         try
         {
            if (!(await _authService.AuthorizeAsync(_principal, Policy)).Succeeded)
               output.SuppressOutput();
         }
         catch (Exception)
         {
            output.SuppressOutput();
         }
      }
   }
}

