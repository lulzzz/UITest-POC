using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using Moq;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.IntegrationTests.Proxies
{
    public class AgencyBudgetProxyTest
    {
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfiguration;
        private readonly RootConfigurations _configuration;
        private readonly AgencyBudgetProxy _agencyBudgetProxy;

        public AgencyBudgetProxyTest()
        {
            _configuration = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());
            _rootConfiguration = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _rootConfiguration.Setup(m => m.Value).Returns(_configuration);
            _agencyBudgetProxy = new AgencyBudgetProxy(_rootConfiguration.Object);
        }

        [Fact]
        public async Task ShouldGetAgencyProjectBudget()
        {
            //Act
            var resultModel = await _agencyBudgetProxy.GetAgencyProjectBudget("211111", true, "022001000000");
            //Assert
            Assert.NotNull(resultModel);
        }
    }
}
