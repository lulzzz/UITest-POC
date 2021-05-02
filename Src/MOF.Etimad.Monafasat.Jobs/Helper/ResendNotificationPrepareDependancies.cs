using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.Jobs
{
    public class ResendNotificationPrepareDependancies
    {
        public ServiceCollection SetupDependancies()
        {
            IConfiguration config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", true, true)
              .Build();
            var services = new ServiceCollection();
            services.AddScoped<INotificationResendJobService, NotificationResendJobService>();
            services.AddScoped<INotificationQueries, NotificationQueries>();
            services.AddScoped<IINotificationCommands, NotificationCommands>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<INotificationProxy, NotificationProxy>();
            services.Configure<RootConfigurations>(config.GetSection("RootConfigurations"));
            services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Scoped);

            return services;
        }
    }
}
