using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class SpendingCategoryTest
    {
        [Fact]
        public void Should_Construct_SpendingCategory()
        {
            SpendingCategory spendingCategory = new SpendingCategory();
            _ = spendingCategory.SpendingCategoryId;
            _ = spendingCategory.NameAr;
            _ = spendingCategory.NameEn;

            spendingCategory.ShouldNotBeNull();
        }
    }
}
