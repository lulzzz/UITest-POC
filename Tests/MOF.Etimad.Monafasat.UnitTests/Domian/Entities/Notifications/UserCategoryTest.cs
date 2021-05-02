using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Notifications
{
    public class UserCategoryTest
    {
        private const string categoryName = "name";
        private const string summary = "summary";

        [Fact]
        public void Should_Construct_UserCategory()
        {
            UserCategory userCategory = new UserCategory(categoryName, summary);
            _ = new UserCategory();
            _ = userCategory.Id;
            userCategory.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_UserCategory_Second()
        {
            UserCategory userCategory = new UserCategory(categoryName, summary, new System.Collections.Generic.List<UserProfile>() { new UserProfile() });
            _ = new UserCategory();
            userCategory.ShouldNotBeNull();
        }
    }
}
