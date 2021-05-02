using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class AgreementTypeTest
    {
        private const bool isActive = true;
        private const string nameAr = "Ar";
        private const string nameEn = "En";

        [Fact]
        public void Should_Construct_AgreementType()
        {
            AgreementType agreementType = new AgreementType(nameEn, nameAr, isActive);
            _ = new AgreementType();
            _ = agreementType.AgreementTypeId;
            _ = agreementType.NameAr;
            _ = agreementType.NameEn;

            agreementType.ShouldNotBeNull();
        }
    }
}
