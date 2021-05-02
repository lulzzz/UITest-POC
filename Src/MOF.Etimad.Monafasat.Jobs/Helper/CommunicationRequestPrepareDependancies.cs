using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Data.GenericRepository;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.Services.JobServices;
using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.Jobs
{
    public class CommunicationRequestPrepareDependancies
    {
        public ServiceCollection SetupDependancies()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var services = new ServiceCollection();
            services.Configure<RootConfigurations>(config.GetSection("RootConfigurations"));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(config);
            services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Scoped);
            services.AddScoped<IGenericCommandRepository, GenericCommandRepository>();
            services.AddScoped<ICommunicationRequestJobQueries, CommunicationRequestJobQueries>();
            services.AddScoped<ICommunicationRequestAppJobService, CommunicationRequestAppJobService>();
            services.AddScoped<INotificationJobAppService, NotificationJobAppService>();
            services.AddScoped<INotificationQueries, NotificationQueries>();
            services.AddScoped<IINotificationCommands, NotificationCommands>();
            services.AddLogging();
            services.AddAutoMapper(typeof(CommunicationRequestPrepareDependancies));
            services.AddScoped<IIDMProxy, IDMProxy>();
            services.AddMemoryCache();
            return services;
        }
    }
}
