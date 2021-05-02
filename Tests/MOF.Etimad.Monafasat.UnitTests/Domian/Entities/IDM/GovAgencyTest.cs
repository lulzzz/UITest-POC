using MOF.Etimad.Monafasat.Core;
using Shouldly;
using Xunit;


namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.IDM
{
    public class GovAgencyTest
    {
        private const string agencyCode = "code";
        private const string arabicName = "name";
        private const int agencyLogoReferenceId = 1;
        private const bool isVro = true;
        private const int categoryId = 1;
        private const string mobileNumber = "768768";
        private const string vrOfficeCode = "0000";

        [Fact]
        public void Should_Construct_GovAgency()
        {
            GovAgency govAgency = new GovAgency(agencyCode, arabicName, agencyLogoReferenceId, isVro, categoryId, mobileNumber, vrOfficeCode);
            _ = new GovAgency();
            _ = govAgency.NameEnglish;
            _ = govAgency.IsDeleted;
            _ = govAgency.IsPrimary;
            _ = govAgency.IsUGP;
            _ = govAgency.RowVersion;
            _ = govAgency.Branches;
            _ = govAgency.Committees;
            _ = govAgency.Tenders;
            _ = govAgency.VROOfficeCodeRelated;

            govAgency.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_GovAgency_Second()
        {
            GovAgency govAgency = new GovAgency(agencyCode, arabicName, agencyLogoReferenceId, false, categoryId, mobileNumber, vrOfficeCode);
            govAgency.ShouldNotBeNull();
        }

        [Fact]
        public void Should_GovAgencyUpdated()
        {
            GovAgency govAgency = new GovAgency(agencyCode, arabicName, agencyLogoReferenceId, isVro, categoryId, mobileNumber, vrOfficeCode);
            govAgency.GovAgencyUpdated(agencyCode, "", arabicName, categoryId, mobileNumber);
            Assert.Equal(agencyCode, govAgency.AgencyCode);
        }

        [Fact]
        public void Should_SetExcepted()
        {
            GovAgency govAgency = new GovAgency(agencyCode, arabicName, agencyLogoReferenceId, isVro, categoryId, mobileNumber, vrOfficeCode);
            bool isExpected = true;
            govAgency.SetExcepted(isExpected);
            Assert.True(govAgency.IsExcepted);
        }

        [Fact]
        public void Should_SetIsOld()
        {
            GovAgency govAgency = new GovAgency(agencyCode, arabicName, agencyLogoReferenceId, isVro, categoryId, mobileNumber, vrOfficeCode);
            bool isOld = true;
            govAgency.SetIsOld(isOld);
            Assert.True(govAgency.IsOldSystem);
        }

        [Fact]
        public void Should_SetPurchaseMethod()
        {
            GovAgency govAgency = new GovAgency(agencyCode, arabicName, agencyLogoReferenceId, isVro, categoryId, mobileNumber, vrOfficeCode);
            string method = "1,2";
            govAgency.SetPurchaseMethod(method);
            Assert.Equal(method, govAgency.PurchaseMethods);
        }

    }
}
