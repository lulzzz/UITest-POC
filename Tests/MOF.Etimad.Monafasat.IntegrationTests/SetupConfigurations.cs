using Microsoft.Extensions.Configuration;
using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.IntegrationTests
{
    public static class SetupConfigurations
    {
        public static RootConfigurations GetApplicationConfiguration(string outputPath)
        {
            var configuration = new RootConfigurations();

            var iConfig = GetIConfigurationRoot(outputPath);

            iConfig
                .GetSection("RootConfigurations")
                .Bind(configuration);

            return configuration;
        }

        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
            .SetBasePath(outputPath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        }
    }
}
