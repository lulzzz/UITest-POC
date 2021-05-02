using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.JobService
{
    public class CommunicationJobQueriesTests
    {
        private readonly CommunicationRequestJobQueries _sut;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfigurationMock;

        public CommunicationJobQueriesTests()
        {

            _rootConfigurationMock = new Mock<IOptionsSnapshot<RootConfigurations>>();

            _sut = new CommunicationRequestJobQueries(InitialData.context, _rootConfigurationMock.Object);
        }

        [Fact]
        public async Task ShouldGetExtendOfferValiditySuppliers()
        {
            InitialData.context.AgencyCommunicationRequests.FirstOrDefault().Tender.UpdateAwardingStoppingPeriod(2);
            var result = await _sut.GetExtendOfferValiditySuppliers();
            Assert.Empty(result);
        }


    }
}
