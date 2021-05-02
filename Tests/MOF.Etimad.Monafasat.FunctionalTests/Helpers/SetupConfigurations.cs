using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using Moq;

namespace MOF.Etimad.Monafasat.FunctionalTests.Helpers
{
    public static class SetupConfigurations
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
            .SetBasePath(outputPath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
        }

        public static Mock<IOptionsSnapshot<RootConfigurations>> GetApplicationConfiguration(string outputPath)
        {
            var rootConfigurations = new RootConfigurations();

            var iConfig = GetIConfigurationRoot(outputPath);

            iConfig
                .GetSection("RootConfigurations")
                .Bind(rootConfigurations);

            Mock<IOptionsSnapshot<RootConfigurations>> rootConfigurationsMock = new Mock<IOptionsSnapshot<RootConfigurations>>();
            rootConfigurationsMock.Setup(m => m.Value).Returns(rootConfigurations);

            return rootConfigurationsMock;
        }
    }
}
