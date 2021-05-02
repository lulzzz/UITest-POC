using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class RequestsRejectionTypeTest
    {
        private const string nameAr = "name";
        private const string nameEn = "name";
        private const bool isActive = true;

        [Fact]
        public void Should_Construct_RequestsRejectionType()
        {
            RequestsRejectionType requestsRejectionType = new RequestsRejectionType(nameEn, nameAr, isActive);
            _ = requestsRejectionType.RequestTypeId;
            _ = new RequestsRejectionType();
            requestsRejectionType.ShouldNotBeNull();
        }
    }
}
