using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class SyncDataWithOldMonafasatTest
    {
        private readonly int _tenderId = 100;
        private readonly string _requestInformation = "request Info";
        private readonly bool _isSendSuccessfully = true;

        [Fact]
        public void Should_Empty_Construct_SyncDataWithOldMonafasat()
        {
            var syncDataWithOldMonafasat = new SyncDataWithOldMonafasat();
            syncDataWithOldMonafasat.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Constructor_SetValues()
        {
            var syncDataWithOldMonafasat = new SyncDataWithOldMonafasat(_tenderId, _requestInformation, _isSendSuccessfully);
            syncDataWithOldMonafasat.ShouldNotBeNull();
            syncDataWithOldMonafasat.TenderId.ShouldBe(_tenderId);
            syncDataWithOldMonafasat.RequestInformation.ShouldBe(_requestInformation);
            syncDataWithOldMonafasat.IsSendSuccessfully.ShouldBeTrue();
        }
    }
}
