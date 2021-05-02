using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class ActivityTemplateTest
    {
        [Fact]
        public void Should_Construct_ActivityTemplate()
        {
            Template activityTemplate = new Template();
            _ = activityTemplate.NameAr;
            _ = activityTemplate.NameEn;
            _ = activityTemplate.ActivitytemplatId;
            _ = activityTemplate.HasYears;
            _ = activityTemplate.ConditionTemplateActivities;

            activityTemplate.ShouldNotBeNull();
        }
    }
}
