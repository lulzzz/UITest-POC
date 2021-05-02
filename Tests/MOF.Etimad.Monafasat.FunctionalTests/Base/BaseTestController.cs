using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using MOF.Etimad.Monafasat.FunctionalTests.Helpers;
using MOF.Etimad.Monafasat.Web;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.FunctionalTests.Base
{
    public class BaseTestController : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public readonly HttpClient _client;
        protected dynamic token;
        public static readonly string AntiForgeryFieldName = "__RequestVerificationToken";
        public static readonly string AntiForgeryCookieName = "AFTCookie";
        public BaseTestController(CustomWebApplicationFactory<Startup> factory)
        {

            _factory = factory;
            //token = new ExpandoObject();
            //token.NewRoles = RoleNames.MonafasatAdmin;
            //token.BranchId = IdentityConfigs.Claims.BranchId;
            //token.CommitteeId = IdentityConfigs.Claims.CommitteeId;
            //token.GovAgencyRoles = IdentityConfigs.Claims.SemiGovRole;
            //token.ContentType = "application/json";
            _client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                            "Test", options => { });

                });
                builder.ConfigureServices(x =>
                {
                    x.AddAntiforgery(T =>
                    {
                        T.Cookie.Name = AntiForgeryCookieName;
                        T.FormFieldName = AntiForgeryFieldName;
                    });
                })
              .UseApplicationInsights();
            }).CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true
            });
            _client.DefaultRequestHeaders.Accept.Clear();
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");


            //_client.SetFakeBearerToken((object)token);
        }

        public async Task<(string fieldValue, string cookieValue)> ExtractAntiForgeryValue(HttpResponseMessage response)
        {
            return (ExtractAntiforgeryToken(await response.Content.ReadAsStringAsync()), ExtractAntiforgeryCookieValueFrom(response));
        }

        private string ExtractAntiforgeryToken(string htmlBody)
        {

            var requestVerificationTokenMatch = Regex.Match(htmlBody, $@"\<input name=""{AntiForgeryFieldName}"" type=""hidden"" value=""([^""]+)"" \/\>");

            if (requestVerificationTokenMatch.Success)
            {
                return requestVerificationTokenMatch.Groups[1].Captures[0].Value;
            }

            throw new ArgumentException($"Anti forgery token'{AntiForgeryFieldName}'not found in html", nameof(htmlBody));
        }

        private string ExtractAntiforgeryCookieValueFrom(HttpResponseMessage response)
        {
            string antiForgeryCookie = response.Headers.GetValues("Set-Cookie")
                .FirstOrDefault(x => x.Contains(AntiForgeryCookieName));

            if (antiForgeryCookie is null)
            {
                throw new ArgumentException($"Kookie'{AntiForgeryCookieName}'not found in http response", nameof(response));
            }

            string antiForgeryCookieValue = SetCookieHeaderValue.Parse(antiForgeryCookie).Value.ToString();
            return antiForgeryCookieValue;
        }
    }
}
