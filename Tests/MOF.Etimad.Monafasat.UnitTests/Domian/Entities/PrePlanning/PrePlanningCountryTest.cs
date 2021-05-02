using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.PrePlanning
{
    public class PrePlanningCountryTest
    {
        private const int countryId = 1;

        [Fact]
        public void Should_Construct_PrePlanningArea()
        {
            PrePlanningCountry prePlanningCountry = new PrePlanningCountry(countryId);
            _ = new PrePlanningCountry();
            _ = prePlanningCountry.Id;
            _ = prePlanningCountry.Country;
            _ = prePlanningCountry.PrePlanningId;
            _ = prePlanningCountry.PrePlanning;
            prePlanningCountry.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Delete()
        {
            PrePlanningCountry prePlanningCountry = new PrePlanningCountry(countryId);
            prePlanningCountry.Delete();
            prePlanningCountry.State.ShouldBe(ObjectState.Deleted);
        }

        [Fact]
        public void Should_Update()
        {
            PrePlanningCountry prePlanningCountry = new PrePlanningCountry(countryId);
            prePlanningCountry.Update();
            prePlanningCountry.ShouldNotBeNull();
        }

        [Fact]
        public void Should_DeActive()
        {
            PrePlanningCountry prePlanningCountry = new PrePlanningCountry(countryId);
            prePlanningCountry.DeActive();
            Assert.False(prePlanningCountry.IsActive);
        }

        [Fact]
        public void Should_SetAddedState()
        {
            PrePlanningCountry prePlanningCountry = new PrePlanningCountry(countryId);
            prePlanningCountry.SetAddedState();
            Assert.Equal(0, prePlanningCountry.Id);
        }

        [Fact]
        public void Should_SetActive()
        {
            PrePlanningCountry prePlanningCountry = new PrePlanningCountry(countryId);
            prePlanningCountry.SetActive();
            Assert.True(prePlanningCountry.IsActive);
        }
    }
}
