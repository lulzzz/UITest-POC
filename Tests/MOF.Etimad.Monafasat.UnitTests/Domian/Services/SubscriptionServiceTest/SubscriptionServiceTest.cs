using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.SubscriptionServiceTest
{
    public class SubscriptionServiceTest
    {
        private readonly Mock<ISubscriptionProxy> _moqSubscriptionProxy;
        private readonly SubscriptionService _sut;

        public SubscriptionServiceTest()
        {
            _moqSubscriptionProxy = new Mock<ISubscriptionProxy>();
            _sut = new SubscriptionService(_moqSubscriptionProxy.Object);
        }


        [Fact]
        public async Task ShouldGetContractorDetailsInquirySuccess()
        {
            _moqSubscriptionProxy.Setup(s => s.GetCRsSubscriptionStatuses(It.IsAny<List<string>>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<SubscriptionModel>>(new List<SubscriptionModel> ());
                });

            var result = await _sut.GetCRsSubscriptionStatuses(new List<string>() {"cr"});
            Assert.NotNull(result);
        }

    }
}
