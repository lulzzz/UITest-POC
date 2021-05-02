using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using System.Linq;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class ActivityTest
    {
        [Fact]
        public void Should_Construct_Activity()
        {
            Activity activity = new Activity();
            _ = activity.ActivityId;
            _ = activity.NameAr;
            _ = activity.NameEn;
            _ = activity.ParentID;
            _ = activity.ParentActivity;
            _ = activity.SubActivities;
            _ = activity.TenderActivities;
            _ = activity.ActivityTemplateVersions.Select(c=>c.Template);
            _ = activity.ActivityTemplateVersions.Select(c=>c.TemplateId);

            activity.ShouldNotBeNull();
        }
    }
}
