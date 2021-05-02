using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class ConditionsTemplateSectionTest
    {
        [Fact]
        public void Should_Construct_ConditionsTemplateSection()
        {
            ConditionsTemplateSection conditionsTemplateSection = new ConditionsTemplateSection();
            _ = conditionsTemplateSection.ConditionsTemplateSectionId;
            _ = conditionsTemplateSection.NameAr;
            _ = conditionsTemplateSection.NameEn;
            _ = conditionsTemplateSection.ConditionTemplateActivities;

            conditionsTemplateSection.ShouldNotBeNull();
        }
    }
}
