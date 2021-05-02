using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using Moq;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.IntegrationTests.Proxies
{
    public class SMEASizeInquiryProxyTest
    {
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfiguration;
        private readonly RootConfigurations _configuration;
        private readonly SMEASizeInquiryProxy _sMEASizeInquiryProxy;


        public SMEASizeInquiryProxyTest()
        {
            _configuration = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());
            _rootConfiguration = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _rootConfiguration.Setup(m => m.Value).Returns(_configuration);

            _sMEASizeInquiryProxy = new SMEASizeInquiryProxy(_rootConfiguration.Object);
        }

        [Fact]
        public async Task ShouldSMEASizeInquiry()
        {
            //Act
            var result = await _sMEASizeInquiryProxy.SMEASizeInquiry("1010000154");
            //var result = await _sMEASizeInquiryProxy.SMEASizeInquiry("1059444180");

            //Assert
            Assert.NotNull(result);
        }
    }
}
