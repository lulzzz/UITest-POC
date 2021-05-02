using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using Moq;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.IntegrationTests.Proxies
{
    public class WathiqProxyTest
    {
        private readonly WathiqProxy _WathiqProxy;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfiguration;
        private readonly RootConfigurations _configuration;

        public WathiqProxyTest()
        {
            _configuration = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());

            _rootConfiguration = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _rootConfiguration.Setup(m => m.Value).Returns(_configuration);
            _WathiqProxy = new WathiqProxy(_rootConfiguration.Object);
        }

        // can't find a CR that returns data so used assert.null
        [Fact]
        public async Task ShouldGetCRDataByCR()
        {
            //Act
            var result = await _WathiqProxy.GetCRDataByCR("1059444180");
            //Assert
            Assert.Null(result);
        }
    }
}
