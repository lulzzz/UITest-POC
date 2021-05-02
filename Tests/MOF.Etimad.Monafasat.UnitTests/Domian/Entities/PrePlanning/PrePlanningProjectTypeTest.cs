using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.PrePlanning
{
    public class PrePlanningProjectTypeTest
    {
        private const int projectTypeId = 1;

        [Fact]
        public void Should_Construct_PrePlanningProjectType()
        {
            PrePlanningProjectType prePlanningProjectType = new PrePlanningProjectType(projectTypeId);
            _ = new PrePlanningProjectType();
            _ = prePlanningProjectType.Id;
            _ = prePlanningProjectType.Activity;
            _ = prePlanningProjectType.PrePlanningId;
            _ = prePlanningProjectType.PrePlanning;
            prePlanningProjectType.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Delete()
        {
            PrePlanningProjectType prePlanningProjectType = new PrePlanningProjectType(projectTypeId);
            prePlanningProjectType.Delete();
            prePlanningProjectType.State.ShouldBe(ObjectState.Deleted);
        }

        [Fact]
        public void Should_Update()
        {
            PrePlanningProjectType prePlanningProjectType = new PrePlanningProjectType(projectTypeId);
            prePlanningProjectType.Update();
            prePlanningProjectType.ShouldNotBeNull();
        }

        [Fact]
        public void Should_DeActive()
        {
            PrePlanningProjectType prePlanningProjectType = new PrePlanningProjectType(projectTypeId);
            prePlanningProjectType.DeActive();
            Assert.False(prePlanningProjectType.IsActive);
        }

        [Fact]
        public void Should_SetAddedState()
        {
            PrePlanningProjectType prePlanningProjectType = new PrePlanningProjectType(projectTypeId);
            prePlanningProjectType.SetAddedState();
            Assert.Equal(0, prePlanningProjectType.Id);
        }

        [Fact]
        public void Should_SetActive()
        {
            PrePlanningProjectType prePlanningProjectType = new PrePlanningProjectType(projectTypeId);
            prePlanningProjectType.SetActive();
            Assert.True(prePlanningProjectType.IsActive);
        }
    }
}
