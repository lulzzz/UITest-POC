using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MOF.Etimad.Monafasat.Data;
using System.IO;
using System.Linq;

namespace MOF.Etimad.Monafasat.FunctionalTests.Helpers
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();
            var _connection = _configuration.GetSection("ConnectionStrings:DefaultConnection").Value;

            builder.ConfigureServices(services =>
            {
                services.AddAuthentication(options =>
                {

                });
                // Remove the app's ApplicationDbContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add a database context using an in-memory 
                // database for testing.
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(_connection);
                });

                // Register test services

                // Build the service provider
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (ApplicationDbContext).
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<AppDbContext>();
                var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                // Ensure the database is created.
                context.Database.EnsureCreated();


            });
        }

    }
}
