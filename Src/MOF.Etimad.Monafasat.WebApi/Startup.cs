using AutoMapper;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.WebApi.Authorization;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Principal;

namespace MOF.Etimad.Monafasat.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IConfigurationRoot ConfigurationRoot { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RootConfigurations>(Configuration.GetSection("RootConfigurations"));
            services.AddSession();
            #region Identity

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = Configuration.GetSection("RootConfigurations:AuthorityConfiguration:AuthorityURL").Value;
                options.ApiName = Configuration.GetSection("RootConfigurations:AuthorityConfiguration:ApiName").Value;
                options.ApiSecret = Configuration.GetSection("RootConfigurations:AuthorityConfiguration:ApiSecret").Value;
                options.RequireHttpsMetadata = false;
            });
            // ======================Policies  ===============================================================
            services.AddDistributedMemoryCache();
            AuthorizationPolicies.LoadAuthorizationInjection(services);
            services.AddAuthorization(authorizationOption => AuthorizationPolicies.LoadPolicies(authorizationOption));
            //===========================================================================================================
            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
            services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), opt => opt.EnableRetryOnFailure())
                                                                                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).EnableSensitiveDataLogging());

            // =======================All Services==============
            services.SetAllConfiguration(Configuration);
            //================================================

            services.AddAutoMapper(typeof(Startup));
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
            });
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("ar-SA");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("ar-SA"), new CultureInfo("en-US") };
                options.RequestCultureProviders.Clear();
            });
            services.AddMemoryCache();
            services.AddControllers(o =>
            {
                o.EnableEndpointRouting = false;
                o.Filters.Add(new CultureSettingResourceFilter());
            })
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ar-SA"),
                SupportedCultures = new List<CultureInfo> { new CultureInfo("ar-SA"), new CultureInfo("en-US") },
                RequestCultureProviders = new List<IRequestCultureProvider>
                { new CookieRequestCultureProvider { CookieName = "language"} }
            });
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
