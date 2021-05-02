using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class VendorCertificatesTest
    {
        [Fact]
        public void Should_Construct_VendorCertificates()
        {
            VendorCertificates vendorCertificates = new VendorCertificates();
            _ = vendorCertificates.VendorCertificateId;
            _ = vendorCertificates.NameAr;
            _ = vendorCertificates.NameEn;
            _ = vendorCertificates.ConditionsTemplateCertificates;

            vendorCertificates.ShouldNotBeNull();
        }
    }
}
