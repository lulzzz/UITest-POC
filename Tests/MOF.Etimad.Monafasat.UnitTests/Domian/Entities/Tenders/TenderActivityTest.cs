using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderActivityTest
    {
        private readonly int _activityId = 100;

        [Fact]
        public void Should_Empty_Construct_TenderActivity()
        {
            var tenderActivity = new TenderActivity();
            tenderActivity.ShouldNotBeNull();
        }

        [Fact]
        public void Should_SetTenderActivityId()
        {
            var tenderActivity = new TenderActivity(_activityId);
            tenderActivity.ShouldNotBe(null);
            tenderActivity.ActivityId.ShouldBe(_activityId);
            tenderActivity.State.ShouldBe(SharedKernal.ObjectState.Added);
        }
        [Fact]
        public void Should_UpdateEntity()
        {
            var tenderActivity = new TenderActivity();
            tenderActivity.Update();
            tenderActivity.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_DeActiveEntity()
        {
            var tenderActivity = new TenderActivity();
            tenderActivity.DeActive();
            tenderActivity.IsActive.ShouldBe(false);
            tenderActivity.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_SetActive()
        {
            var tenderActivity = new TenderActivity();
            tenderActivity.SetActive();
            tenderActivity.IsActive.ShouldBe(true);
            tenderActivity.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_SetAddedState()
        {
            var tenderActivity = new TenderActivity();
            tenderActivity.SetAddedState();
            tenderActivity.TenderActivityId.ShouldBe(0);
            tenderActivity.State.ShouldBe(SharedKernal.ObjectState.Added);
        }
    }
}
