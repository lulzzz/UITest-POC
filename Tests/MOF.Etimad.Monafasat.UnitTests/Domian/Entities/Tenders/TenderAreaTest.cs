using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderAreaTest
    {
        private readonly int _areaId = 10;

        [Fact]
        public void Should_Empty_Construct_TenderArea()
        {
            var tenderArea = new TenderArea();
            tenderArea.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Constructor_SetAreaId()
        {
            var tenderArea = new TenderArea(_areaId);
            tenderArea.ShouldNotBeNull();
            tenderArea.AreaId.ShouldBe(_areaId);
            tenderArea.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_DeleteEntity()
        {
            var tenderArea = new TenderArea();
            tenderArea.Delete();
            tenderArea.State.ShouldBe(SharedKernal.ObjectState.Deleted);
        }

        [Fact]
        public void Should_DeactivateEntity()
        {
            var tenderArea = new TenderArea();
            tenderArea.DeActive();
            tenderArea.IsActive.ShouldBe(false);
            tenderArea.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_SetActive()
        {
            var tenderArea = new TenderArea();
            tenderArea.SetActive();
            tenderArea.IsActive.ShouldBe(true);
            tenderArea.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_SetAddedState()
        {
            var tenderArea = new TenderArea();
            tenderArea.SetAddedState();
            tenderArea.Id.ShouldBe(0);
            tenderArea.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_Update()
        {
            var tenderArea = new TenderArea();
            tenderArea.Update();
            tenderArea.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }
    }
}
