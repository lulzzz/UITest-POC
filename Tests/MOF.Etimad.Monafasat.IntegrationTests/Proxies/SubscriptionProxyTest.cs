using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration.Proxies;
using MOF.Etimad.Monafasat.SharedKernal;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.IntegrationTests.Proxies
{
    public class SubscriptionProxyTest
    {
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfiguration;
        private readonly RootConfigurations _configuration;
        private readonly SubscriptionProxy _subscriptionProxy;


        public SubscriptionProxyTest()
        {
            _configuration = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());
            _rootConfiguration = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _rootConfiguration.Setup(m => m.Value).Returns(_configuration);

            _subscriptionProxy = new SubscriptionProxy(_rootConfiguration.Object);
        }

        [Fact]
        public async Task ShouldGetCRsSubscriptionStatuses()
        {
            //Arrange
            List<string> CRs = new List<string> { "1059444180", "1024901843 " };

            //Act
            var result = await _subscriptionProxy.GetCRsSubscriptionStatuses(CRs);

            //Assert
            Assert.NotNull(result);
        }
    }
}
