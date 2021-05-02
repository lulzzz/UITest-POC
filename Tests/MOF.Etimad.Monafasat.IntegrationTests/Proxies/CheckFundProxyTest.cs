using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Integration.Enums;
using MOF.Etimad.Monafasat.SharedKernal;
using Moq;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.IntegrationTests.Proxies
{
    public class CheckFundProxyTest
    {
        private readonly CheckFundProxy _CheckFundProxy;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfiguration;
        private readonly RootConfigurations _configuration;
        public CheckFundProxyTest()
        {
            _configuration = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());

            _rootConfiguration = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _rootConfiguration.Setup(m => m.Value).Returns(_configuration);
            _CheckFundProxy = new CheckFundProxy(_rootConfiguration.Object);
        }

        [Theory]
        [InlineData(BudgetType.Cash)]
        [InlineData(BudgetType.Cost)]
        [InlineData(BudgetType.Income)]

        public async Task ShouldGetProjectBudgetByType(BudgetType budgetType)
        {
            var result = await _CheckFundProxy.GetProjectBudgetByType("022001000000", "211111", budgetType, true);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(BudgetType.Cash)]
        [InlineData(BudgetType.Income)]

        public async Task ShouldGetProjectBudgetByTypeSucess(BudgetType budgetType)
        {
            Project project = new Project() { Agency= "022001000000", ProjectName="projectName" };
            var result = await _CheckFundProxy.GetProjectBudgetByType(project, budgetType,true);
            Assert.NotNull(result);
        }
    }
}
