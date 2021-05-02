using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderAwardingHistoryTest
    {
        private readonly string _commercialRegisterationNumber = "CR6006";
        private readonly int _tenderId = 2001;
        private readonly bool _tenderAwardingType = true;
        private readonly decimal _awardingValue = 2002;
        private readonly int _awardingIndex = 2003;

        [Fact]
        public void Should_Empty_Construct_TenderAwardingHistory()
        {
            var tenderAwardingHistory = new TenderAwardingHistory();
            tenderAwardingHistory.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Contsructor_SetValues()
        {
            var tenderAwardingHistory = new TenderAwardingHistory(_commercialRegisterationNumber, _tenderId, _tenderAwardingType, _awardingValue, _awardingIndex);
            tenderAwardingHistory.ShouldNotBeNull();
            tenderAwardingHistory.CommercialRegisterationNumber.ShouldBe(_commercialRegisterationNumber);
            tenderAwardingHistory.TenderId.ShouldBe(_tenderId);
            tenderAwardingHistory.TenderAwardingType.ShouldBe(_tenderAwardingType);
            tenderAwardingHistory.AwardingValue.ShouldBe(_awardingValue);
            tenderAwardingHistory.AwardingIndex.ShouldBe(_awardingIndex);
            tenderAwardingHistory.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_Update()
        {
            var tenderAwardingHistory = new TenderAwardingHistory();
            tenderAwardingHistory.Update(_commercialRegisterationNumber, _tenderId, _tenderAwardingType, _awardingValue, _awardingIndex);
            tenderAwardingHistory.ShouldNotBeNull();
            tenderAwardingHistory.CommercialRegisterationNumber.ShouldBe(_commercialRegisterationNumber);
            tenderAwardingHistory.TenderId.ShouldBe(_tenderId);
            tenderAwardingHistory.TenderAwardingType.ShouldBe(_tenderAwardingType);
            tenderAwardingHistory.AwardingValue.ShouldBe(_awardingValue);
            tenderAwardingHistory.AwardingIndex.ShouldBe(_awardingIndex);
            tenderAwardingHistory.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }
    }
}
