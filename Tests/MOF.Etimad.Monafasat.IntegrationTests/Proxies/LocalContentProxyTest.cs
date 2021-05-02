using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using Moq;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.IntegrationTests.Proxies
{
    public class LocalContentProxyTest
    {
        private readonly LocalContentProxy _localContentProxy;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfiguration;
        private readonly RootConfigurations _configuration;

        public LocalContentProxyTest()
        {
            _configuration = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());

            _rootConfiguration = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _rootConfiguration.Setup(m => m.Value).Returns(_configuration);
            _localContentProxy = new LocalContentProxy(_rootConfiguration.Object);
        }

        [Fact]
        public async Task ShouldGetBaselineScore_Error()
        {
            string supplierId = "1323726";
            //Act
            var result = await _localContentProxy.GetBaselineScoreInquiry(supplierId);
            //Assert
            Assert.NotNull(result);
            Assert.False(result.isSuccess);
            Assert.Equal("لا يوجد", result.msg);
        }
                
        [Fact]
        public async Task ShouldGetBaselineScoreInquiry_Success()
        {
            string supplierId = "1010126649";
            //Act
            var result = await _localContentProxy.GetBaselineScoreInquiry(supplierId);
            //Assert
            Assert.NotNull(result);
            Assert.True(result.isSuccess);
            Assert.Equal("22.12", result.content.ToString());
        }



        [Fact]
        public async Task ShouldGetTargetPlanScore_Error()
        {
            string supplierId = "1323726";
            string tenderId = "222";
            //Act
            var result = await _localContentProxy.GetTargetPlanScoreInquiry(supplierId, tenderId);
            //Assert
            Assert.NotNull(result);
            Assert.False(result.isSuccess);
            Assert.Equal("لا يوجد", result.msg);
        }


        [Fact]
        public async Task ShouldGetTargetPlanScore_Success()
        {
            string supplierId = "1010126649";
            string tenderId = "222";
            //Act
            var result = await _localContentProxy.GetTargetPlanScoreInquiry(supplierId, tenderId);
            //Assert
            Assert.NotNull(result);
            Assert.True(result.isSuccess);
            Assert.Equal("64.57", result.content.ToString());
        }
    }
}
