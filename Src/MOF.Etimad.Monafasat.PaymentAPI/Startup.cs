using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Data.GenericRepository;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.PaymentCallbackAPI.MiddlewareHandlers;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using Swashbuckle.AspNetCore.Swagger;
using System.Security.Principal;
namespace MOF.Etimad.Monafasat.PaymentCallbackAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RootConfigurations>(Configuration.GetSection("RootConfigurations"));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
            services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            services.AddScoped<IBillQueries, BillQueries>();
            services.AddScoped<IBillCommand, BillCommand>();
            services.AddScoped<IBillArchiveCommand, BillArchiveCommand>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IIDMAppService, IDMAppService>();
            services.AddScoped<INotificationAppService, NotificationAppService>();
            services.AddScoped<ISupplierCommands, SupplierCommands>();
            services.AddScoped<ISupplierQueries, SupplierQueries>();
            services.AddScoped<IIDMQueries, IDMQueries>();
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<INotificationQueries, NotificationQueries>();
            services.AddScoped<IINotificationCommands, NotificationCommands>();
            services.AddScoped<IGenericCommandRepository, GenericCommandRepository>();
            services.AddScoped<IOfferAppService, OfferAppService>();
            services.AddScoped<IOfferQueries, OfferQueries>();
            services.AddScoped<IOfferCommands, OfferCommands>();
            services.AddScoped<IOfferDomainService, OfferDomainService>();
            services.AddScoped<ITenderCommands, TenderCommands>();
            services.AddScoped<ITenderQueries, TenderQueries>();
            services.AddScoped<ITenderAppService, TenderAppService>();
            services.AddScoped<ICommitteeQueries, CommitteeQueries>();
            services.AddScoped<ILookUpServiceQueries, LookupServiceQueries>();
            services.AddScoped<ITenderDomainService, TenderDomainService>();
            services.AddScoped<IBranchServiceQueries, BranchServiceQueries>();
            services.AddScoped<IBillingProxy, BillingProxy>();
            services.AddMemoryCache();
            services.AddScoped<IBillAppService, BillAppService>();
            services.AddScoped<INotificationProxy, NotificationProxy>();
            services.AddScoped<IIDMProxy, IDMProxy>();
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "Monafasat API", Description = "Monafasat API" }));
            services.AddControllers(o =>
            {
                o.EnableEndpointRouting = false;
            });
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseRouting();
            app.UseBasicAuthenticationMiddleware();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
