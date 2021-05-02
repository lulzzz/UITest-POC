using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.FunctionalTests.Base;
using MOF.Etimad.Monafasat.FunctionalTests.Helpers;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Controllers;
using Moq;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.FunctionalTests.API.Controllers
{
    public class MandatoryListApiControllerTest: BaseTestApiController
    {

        private readonly Mock<IOptionsSnapshot<RootConfigurations>> rootConfigurationsMock;
        private readonly IMandatoryListAppService mandatoryListAppService;
        private readonly MandatoryListController _mandatoryListController;
        private readonly IVerificationService _verification;

        public MandatoryListApiControllerTest()
        {
            var serviceProvider = services.BuildServiceProvider();
            rootConfigurationsMock = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());
            mandatoryListAppService = serviceProvider.GetService<IMandatoryListAppService>();
            _verification = serviceProvider.GetService<IVerificationService>();
            _mandatoryListController = new MandatoryListController(mandatoryListAppService, rootConfigurationsMock.Object, _verification);
        }


        [Fact]
        public async Task GetMandatoryListDataSucess()
        {
            MandatoryListSearchViewModel criteria = new MandatoryListSearchViewModel();
            var response = await _mandatoryListController.Search(criteria);
            Assert.NotNull(response);
        }
        [Fact]
        public async Task GetMandatoryListProjectedFindDataSucess()
        {
            var response = await _mandatoryListController.Find("C16UD9VtkA2QO0MXXLVb9Q==");
            Assert.NotNull(response);
        }
    }
}
