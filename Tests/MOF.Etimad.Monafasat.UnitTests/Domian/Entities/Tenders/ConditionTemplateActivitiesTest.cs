using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class ConditionTemplateActivitiesTest
    {
        [Fact]
        public void Should_Empty_Construct_ConditionTemplateActivities()
        {
            var conditionTemplateActivities = new ConditionTemplateActivities();
            conditionTemplateActivities.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Delete()
        {
            var conditionTemplateActivities = new ConditionTemplateActivities();
            conditionTemplateActivities.Delete();
            conditionTemplateActivities.State.ShouldBe(SharedKernal.ObjectState.Deleted);
        }

        [Fact]
        public void Should_Update()
        {
            var conditionTemplateActivities = new ConditionTemplateActivities();
            conditionTemplateActivities.Update();
            conditionTemplateActivities.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_DeActive()
        {
            var conditionTemplateActivities = new ConditionTemplateActivities();
            conditionTemplateActivities.DeActive();
            conditionTemplateActivities.IsActive.ShouldBe(false);
            conditionTemplateActivities.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_SetAddedState()
        {
            var conditionTemplateActivities = new ConditionTemplateActivities();
            conditionTemplateActivities.SetAddedState();
            conditionTemplateActivities.Id.ShouldBeGreaterThanOrEqualTo(0);
            conditionTemplateActivities.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_SetActive()
        {
            var conditionTemplateActivities = new ConditionTemplateActivities();
            conditionTemplateActivities.SetActive();
            conditionTemplateActivities.IsActive.ShouldBe(true);
            conditionTemplateActivities.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }
    }
}
