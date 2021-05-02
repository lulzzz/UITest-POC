using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Collections.Generic;
using System.Globalization;

namespace MOF.Etimad.Monafasat.Web
{
   public class Startup
   {
      public IConfiguration Configuration { get; }
      public IWebHostEnvironment Env { get; }
      public IConfigurationRoot ConfigurationRoot { get; }
      public Startup(IConfiguration configuration, IWebHostEnvironment env)
      {
         Configuration = configuration;
         Env = env;
         var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();
         ConfigurationRoot = builder.Build();
      }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         services.SetAllConfiguration(Configuration, Env);
         services.ConfigureAuthentication(Configuration);
      }

      public void Configure(IApplicationBuilder app)
      {
         app.UseExceptionMiddleware(Env.EnvironmentName == "Development");
         app.UseStaticFiles();
         app.UseDefaultFiles();
         app.UseRouting();
         app.UseSession();
         app.UseAuthentication();
         app.UseAuthorization();
         app.Use(async (context, next) =>
         {
            context.Response.Headers.Add("X-Frame-Options", "DENY");
            context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
            //context.Response.Headers.Add("Content-Security-Policy", "script-src 'self'; " + "style-src 'self'; " + "img-src 'self'");
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            await next();
         });
         if (Env.EnvironmentName != "Development")
         {
            app.UseHttpsRedirection();
            app.UseHsts();
         }
         app.UseEndpoints(
            endpoints =>
            {
               endpoints.MapControllerRoute("default", "{controller=Tender}/{action=AllTendersForVisitor}/{id?}");
               endpoints.MapHub<BiddingRoundHub>("/biddingRoundHub");
            });
         app.UseRequestLocalization(new RequestLocalizationOptions
         {
            DefaultRequestCulture = new RequestCulture("ar-SA"),
            SupportedCultures = new List<CultureInfo> { new CultureInfo("ar-SA"), new CultureInfo("ar-SA") },
            RequestCultureProviders = new List<IRequestCultureProvider>
            {
               new CookieRequestCultureProvider { CookieName = "language"}
            }
         });
         // Shows UseCors with CorsPolicyBuilder.
         app.UseCors(builder => builder.WithOrigins(Configuration.GetSection("RootConfiguration:APIConfiguration:MonafasatAPI").Value));
      }
   }
}
