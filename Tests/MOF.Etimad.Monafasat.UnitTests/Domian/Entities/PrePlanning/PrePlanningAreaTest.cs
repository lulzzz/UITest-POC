using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.PrePlanning
{
    public class PrePlanningAreaTest
    {
        private const int areaId = 1;

        [Fact]
        public void Should_Construct_PrePlanningArea()
        {
            PrePlanningArea prePlanningArea = new PrePlanningArea(areaId);
            _ = new PrePlanningArea();
            _ = prePlanningArea.Id;
            _ = prePlanningArea.Area;
            _ = prePlanningArea.PrePlanningId;
            _ = prePlanningArea.PrePlanning;
            prePlanningArea.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Delete()
        {
            PrePlanningArea prePlanningArea = new PrePlanningArea(areaId);
            prePlanningArea.Delete();
            prePlanningArea.State.ShouldBe(ObjectState.Deleted);
        }

        [Fact]
        public void Should_Update()
        {
            PrePlanningArea prePlanningArea = new PrePlanningArea(areaId);
            prePlanningArea.Update();
            prePlanningArea.ShouldNotBeNull();
        }

        [Fact]
        public void Should_DeActive()
        {
            PrePlanningArea prePlanningArea = new PrePlanningArea(areaId);
            prePlanningArea.DeActive();
            Assert.False(prePlanningArea.IsActive);
        }

        [Fact]
        public void Should_SetAddedState()
        {
            PrePlanningArea prePlanningArea = new PrePlanningArea(areaId);
            prePlanningArea.SetAddedState();
            Assert.Equal(0, prePlanningArea.Id);
        }

        [Fact]
        public void Should_SetActive()
        {
            PrePlanningArea prePlanningArea = new PrePlanningArea(areaId);
            prePlanningArea.SetActive();
            Assert.True(prePlanningArea.IsActive);
        }

    }
}
